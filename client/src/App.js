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
    const [activeCircles, setActiveCircles] = useState([]);
    const [socket, setSocket] = useState(null);
    const audioRef = useRef(null);
    const timeIsUpRef = useRef(false);

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
                    setActiveCircles(prev => [...new Set([...prev, data.tableId])]);
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
            const delay = Math.floor(Math.random() * (3000 - 1000 + 1)) + 1000;
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
        } else {
            audioRef.current.pause();
        }
    };

    return (
        <div className="App">
            <div className="content-container">
                <div className="media-container">
                    <img src="Musique.png" alt="Description de l'image" className="music-logo" />
                    <button onClick={togglePlay} className="play-pause-button">Play/Pause Music</button>
                    <audio ref={audioRef} src="musique.mp3" />
                </div>
                <div className='circles'>
                    <CirclesGrid activeCircles={activeCircles}/>
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
