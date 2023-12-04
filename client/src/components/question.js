import React from 'react';
import './question.css';

const Response = ({ label, text, isSelected, isCorrect }) => {
    const responseStyle = isSelected
        ? { backgroundColor: isCorrect ? 'green' : 'red' }
        : {};

    return (
        <div className="response" style={responseStyle}>
            <div className="response-label">{label}</div>
            <div className="response-text">{text}</div>
        </div>
    );
};

const Question = ({ questionData, selectedAnswer, isAnswerCorrect }) => {
    if (!questionData || !questionData.question) {
        return <div>Pas encore de questions restez vigilents !</div>;
    }

    return (
        <div className="question-container">
            <div className="question-text">{questionData.question}</div>
            <div className="responses">
                {Object.entries(questionData.answers).map(([label, text]) => (
                    <Response 
                        key={label} 
                        label={label} 
                        text={text}
                        isSelected={label === selectedAnswer}
                        isCorrect={isAnswerCorrect}
                    />
                ))}
            </div>
        </div>
    );
};

export default Question;