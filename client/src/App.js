import React, { useState, useRef, useEffect } from 'react';
import Question from './components/question.js';

const App = () => {
    const [questionData, setQuestionData] = useState({ question: '', answers: {} });
    const [selectedAnswer, setSelectedAnswer] = useState(null);
    const [isAnswerCorrect, setIsAnswerCorrect] = useState(null);
    const [isWaiting, setIsWaiting] = useState(true);
    const [socket, setSocket] = useState(null);
    const audioRef = useRef(null);

    const handleWebSocketMessages = (event) => {
        console.log("Received data:", event.data);
        try {
            const data = JSON.parse(event.data);
            switch (data.type) {
                case 'question':
                    setQuestionData(data.question);
                    setIsAnswerCorrect(null);
                    setSelectedAnswer(null);
                    setIsWaiting(false);
                    break;
                case 'answerResult':
                    setSelectedAnswer(data.answer);
                    setIsAnswerCorrect(data.isCorrect);
                    // Si la réponse est correcte, affichez à nouveau le message d'attente.
                    if (data.isCorrect) {
                        setIsWaiting(true);
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
        const newSocket = new WebSocket('ws://localhost:8080');
        newSocket.onmessage = handleWebSocketMessages;
        newSocket.onopen = () => {
            // Envoie la première demande après 10 secondes
            setTimeout(() => {
                newSocket.send(JSON.stringify({ type: 'requestQuestion' }));
                setIsWaiting(true);
            }, 5000);
        };
        newSocket.onclose = () => console.log('Disconnected from server');
        newSocket.onerror = (error) => console.log('WebSocket error:', error);
        setSocket(newSocket);

        // Supprimer l'intervalle ici
        return () => {
            newSocket.close();
        };
    }, []);

    const togglePlay = () => {
        if (audioRef.current.paused) {
            audioRef.current.play();
        } else {
            audioRef.current.pause();
        }
    };

    return (
        <div className="App">
            <img src="Musique.png" alt="Description de l'image" />
            <button onClick={togglePlay}>Play/Pause Music</button>
            <audio ref={audioRef} src="musique.mp3" />
            {isWaiting ? (
                <div>Restez vigilant, une question peut arriver à n'importe quel moment</div>
            ) : (
                <Question
                    questionData={questionData}
                    selectedAnswer={selectedAnswer}
                    isAnswerCorrect={isAnswerCorrect}
                />
            )}
        </div>
    );
};

export default App;
