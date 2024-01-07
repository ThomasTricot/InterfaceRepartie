import React, { useState, useEffect } from 'react';
import './question.css';

const Response = ({ label, text, responses, isCorrect, timeIsUp }) => {
    const responseStyle = timeIsUp && isCorrect 
        ? { backgroundColor: 'lightgreen' } 
        : { backgroundColor: 'lightgray' };

    return (
        <div className='response selected' style={responseStyle}>
            <div className="response-label">{label}</div>
            <div className="response-text">{text}</div>
            {responses && responses.map((tableResponse, index) => (
                <div key={index} className="table-response-circle" style={{ left: `${20 + 100 * index}px` }}>
                    {tableResponse.tableId} (#{tableResponse.order})
                </div>
            ))}
        </div>
    );
};

const Question = ({ questionData, tableResponses, timeIsUp, timer }) => {   
    const responsesByLabel = Object.values(tableResponses).reduce((acc, response) => {
        (acc[response.answer] = acc[response.answer] || []).push(response);
        return acc;
    }, {});

    Object.keys(responsesByLabel).forEach(label => {
        responsesByLabel[label].sort((a, b) => a.order - b.order);
    });

    if (!questionData || !questionData.question) {
        return <div>Pas encore de questions restez vigilents !</div>;
    }

    return (
        <div className="question-container">
            <div className="timer">
                {timer > 0 ? `${timer}s` : "Temps écoulé"}
            </div>
            <div className="question-text">{questionData.question}</div>
            <div className="responses">
                {Object.entries(questionData.answers).map(([label, text]) => (
                    <Response 
                        key={label} 
                        label={label} 
                        text={text}
                        responses={responsesByLabel[label]}
                        isCorrect={label === questionData.correctAnswer}
                        timeIsUp={timeIsUp}
                    />
                ))}
            </div>
        </div>
    );
};

export default Question;