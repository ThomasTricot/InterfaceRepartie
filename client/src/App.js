import React, { useState, useRef, useEffect } from 'react';
import Question from './components/question.js';
import CirclesGrid from './components/circlegrid.js';
import './App.css'

const App = () => {
    const [questionData, setQuestionData] = useState({ question: '', answers: {} });
    const [tableResponses, setTableResponses] = useState({});
    const [isWaiting, setIsWaiting] = useState(true);
    const [timeIsUp, setTimeIsUp] = useState(false);
    const [timer, setTimer] = useState(30);

    const [teacherCircles, setTeacherCircles] = useState([]);
    const [studentCircles, setStudentCircles] = useState([]);

    const [socket, setSocket] = useState(null);
    const [logoImage, setLogoImage] = useState("resume.png");
    const audioRef = useRef(null);
    const timeIsUpRef = useRef(false);
    const correctSoundRef = useRef(null);

    const handleWebSocketMessages = (event) => {
        console.log("Received data:", event.data);
        try {
            const data = JSON.parse(event.data);
            switch (data.type) {
                case 'question':
                    setQuestionData(data.question);
                    setIsWaiting(false);
                    break;
                case 'tableFinished':
                    setStudentCircles(prev => [...new Set([...prev, Number(data.tableId)])]);
                    setTeacherCircles(prev => {
                        const newCircles = { ...prev };
                        if (newCircles[Number(data.tableId)] !== true) {
                            delete newCircles[Number(data.tableId)];
                        }
                        return newCircles;
                    });
                    break;
                case 'tableFinishedWithCorrect':
                    setTeacherCircles(prev => {
                        const newCircles = { ...prev };
                        newCircles[Number(data.tableId)] = data.correct;
                        return newCircles;
                    });
                    break;
                case 'answerResult':
                    if (!timeIsUpRef.current) {
                        setTableResponses(prevResponses => {
                            const previousAnswer = prevResponses[data.tableId]?.answer;
                            let updatedResponses = { ...prevResponses };
                            if (previousAnswer && previousAnswer !== data.answer) {
                                Object.keys(updatedResponses).forEach(key => {
                                    if (updatedResponses[key].answer === previousAnswer && updatedResponses[key].order > updatedResponses[data.tableId].order) {
                                        updatedResponses[key].order -= 1;
                                    }
                                });
                            }
                        
                            const currentResponseOrder = Object.values(updatedResponses)
                                .filter(response => response.answer === data.answer && response.tableId !== data.tableId)
                                .length + 1;
                        
                            updatedResponses[data.tableId] = {
                                ...data,
                                order: currentResponseOrder
                            };
                        
                            return updatedResponses;
                        });
                    }                                      
                    break;
                default:
                    console.error('Unrecognized message type:', data.type);
            }
        } catch (error) {
            console.error('Error parsing JSON:', error);
        }
    };

    useEffect(() => {
        console.log("Table Responses Updated:", tableResponses);
    }, [tableResponses]);    

    useEffect(() => {
        console.log("question data:", questionData);
    }, [questionData]); 

    useEffect(() => {
        const newSocket = new WebSocket('ws://localhost:8080');
        newSocket.onmessage = handleWebSocketMessages;
        newSocket.onopen = () => {
            const delay = Math.floor(Math.random() * (20 + 1)) + 1;
            setTimeout(() => {
                newSocket.send(JSON.stringify({ type: 'requestQuestion' }));
                setIsWaiting(true);
            }, delay);
        };
        newSocket.onclose = () => console.log('Disconnected from server');
        newSocket.onerror = (error) => console.log('WebSocket error:', error);
        setSocket(newSocket);

        // Supprimer l'intervalle ici
        return () => {
            newSocket.close();
        };
    }, []);

    useEffect(() => {
        let interval;
        if (!isWaiting) {
            setTimer(30);
            setTimeIsUp(false);
            interval = setInterval(() => {
                setTimer(prevTimer => {
                    if (prevTimer === 1) {
                        setTimeIsUp(true);
                    }
                    return prevTimer > 0 ? prevTimer - 1 : 0;
                });
            }, 1000);
        }
        return () => clearInterval(interval);
    }, [isWaiting]);

    useEffect(() => {
        timeIsUpRef.current = timeIsUp;
    }, [timeIsUp]);

    const togglePlay = () => {
        if (audioRef.current.paused) {
            audioRef.current.play();
            setLogoImage("pause.png");
        } else {
            audioRef.current.pause();
            setLogoImage("resume.png");
        }
    };

    return (
        <div className="App">
            <div className="content-container">
                <div className="media-container">
                    <img 
                        src={logoImage} 
                        onClick={togglePlay} 
                        className="music-logo" 
                        alt="Play/Pause"
                    />
                    <audio ref={audioRef} src="musique.mp3" />
                    <audio ref={correctSoundRef} src="correct.mp3" preload="auto" />
                </div>
                <div className='circles'>
                    <CirclesGrid studentCircles={studentCircles} teacherCircles={teacherCircles} />
                </div>
            </div>
            {isWaiting ? (
                <div className='waitingText'>Restez vigilant, une question peut arriver à n'importe quel moment...</div>
            ) : (
                <Question
                    questionData={questionData}
                    tableResponses={tableResponses}
                    timeIsUp={timeIsUp}
                    timer={timer}
                />
            )}
        </div>
    );     
};

export default App;
