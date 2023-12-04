import React, { useState, useEffect } from 'react';
import Question from './components/question.js';

const App = () => {
    const [questionData, setQuestionData] = useState({ question: '', answers: {} });
    const [selectedAnswer, setSelectedAnswer] = useState(null);
    const [isAnswerCorrect, setIsAnswerCorrect] = useState(null);
    const [socket, setSocket] = useState(null);

    useEffect(() => {
        const newSocket = new WebSocket('ws://localhost:8080');

        newSocket.onmessage = (event) => {
            console.log("Received data:", event.data);
            try {
                const data = JSON.parse(event.data);
                if (data.question) {
                    console.log("2");
                    setQuestionData(data);
                } else if (data.answer) {
                    console.log("3");

                    setSelectedAnswer(data.answer);
                    setIsAnswerCorrect(data.isCorrect);
                }
            } catch (error) {
                console.error('Error parsing JSON:', error);
            }
        };

        newSocket.onopen = () => console.log('Connected to server');
        newSocket.onclose = () => console.log('Disconnected from server');
        newSocket.onerror = (error) => console.log('WebSocket error:', error);


        setSocket(newSocket);

        return () => newSocket.close();
    }, []);

    return (
        <div className="App">
            <Question 
                questionData={questionData} 
                selectedAnswer={selectedAnswer}
                isAnswerCorrect={isAnswerCorrect}
            />
        </div>
    );
};

export default App;