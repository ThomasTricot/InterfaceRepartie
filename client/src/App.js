import React, { useState, useEffect } from 'react';
import Question from './components/question.js';

const App = () => {
    const [questionData, setQuestionData] = useState({ question: '', answers: {} });
    const [selectedAnswer, setSelectedAnswer] = useState(null);
    const [isAnswerCorrect, setIsAnswerCorrect] = useState(null);
    const [socket, setSocket] = useState(null);

    const handleClick = () => {
        const newSocket = new WebSocket('ws://localhost:8080');

        newSocket.onmessage = (event) => {
            console.log("Received data:", event.data);
            try {
                const data = JSON.parse(event.data);
                switch (data.type) {
                    case 'question':
                        setQuestionData(data.question);
                        setIsAnswerCorrect(null);
                        setSelectedAnswer(null);
                        break;
                    case 'answerResult':
                        setSelectedAnswer(data.answer);
                        setIsAnswerCorrect(data.isCorrect);
                        break;
                    default:
                        console.error('Unrecognized message type:', data.type);
                }
            } catch (error) {
                console.error('Error parsing JSON:', error);
            }
        };

        newSocket.onopen = () => {
            newSocket.send(JSON.stringify({ type: 'requestQuestion' }));
        };
        newSocket.onclose = () => console.log('Disconnected from server');
        newSocket.onerror = (error) => console.log('WebSocket error:', error);


        setSocket(newSocket);

        return () => newSocket.close();
    };

    return (
        <div className="App">
            <button onClick={handleClick}>Question</button> 
            <Question 
                questionData={questionData} 
                selectedAnswer={selectedAnswer}
                isAnswerCorrect={isAnswerCorrect}
            />       
        </div>
    );
};

export default App;