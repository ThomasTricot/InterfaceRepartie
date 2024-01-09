import React, { useState, useEffect } from 'react';
import './question.css';

const Response = ({ label, text, responses, isCorrect, timeIsUp }) => {
    const responseClasses = `response ${isCorrect ? "selected correct full-size" : "selected"}`;
    const responseStyle = timeIsUp && isCorrect 
        ? { backgroundColor: '#34eb3d' } 
        : { backgroundColor: 'lightgray' };

    const showSeparator = responses && responses.length > 0;
    
    return (
        <div className={responseClasses} style={responseStyle}>
            <div className={`response-content ${showSeparator ? "" : "full-width"}`}>
                <div className="response-label">{label}</div>
                <div className="response-text">{text}</div>
            </div>
            {showSeparator && <div className="response-separator"></div>}
            <div className="response-remaining">
                {responses && responses.map((tableResponse, index) => (
                    <div key={index} className="table-response-circle" style={{ left: `${20 + 100 * index}px` }}>
                        {tableResponse.tableId} (#{tableResponse.order})
                    </div>
                ))}
            </div>
        </div>
    );
};

const Question = ({ questionData, tableResponses, timeIsUp, timer }) => {
    const [soundPlayed, setSoundPlayed] = useState(false);
    const [showProgressBar, setShowProgressBar] = useState(true);
    const [changeResponseColor, setChangeResponseColor] = useState(false);

    const responsesByLabel = Object.values(tableResponses).reduce((acc, response) => {
        (acc[response.answer] = acc[response.answer] || []).push(response);
        return acc;
    }, {});

    Object.keys(responsesByLabel).forEach(label => {
        responsesByLabel[label].sort((a, b) => a.order - b.order);
    });

    /*
    useEffect(() => {
        if (timer <= 5 && !soundPlayed) {
            const audio = new Audio('chrono.mp3');
            audio.play();
            setSoundPlayed(true);
        }

        if (timer === 30) {
            setSoundPlayed(false);
        }
    }, [timer]);
    */

    useEffect(() => {
        if (timer === 0) {
            const timerId = setTimeout(() => {
                setShowProgressBar(false);
                setChangeResponseColor(true);
            }, 1050);

            return () => clearTimeout(timerId);
        }
    }, [timer]);

    let progressBarColor;
    if (timer > 15) {
        progressBarColor = '#34eb3d';
    } else if (timer > 5) {
        progressBarColor = '#ebbd34';
    } else {
        progressBarColor = '#fa5b37';
    }

    if (!questionData || !questionData.question) {
        return <div>Pas encore de questions restez vigilents !</div>;
    }

    const progressBarWidth = timeIsUp ? 100 : ((30 - timer) / 30) * 100;

    return (
        <div className="question-container">
            <div className="question-progress-container">
                {showProgressBar && (
                    <div className="progress-bar-container">
                        <div className="progress-bar"
                            style={{ width: `${progressBarWidth}%`, backgroundColor: progressBarColor }}>
                        </div>
                    </div>
                )}
                <div className="question-text">{questionData.question}</div>
            </div>
            <div className="responses">
                {Object.entries(questionData.answers).map(([label, text]) => (
                    <Response 
                        key={label} 
                        label={label} 
                        text={text}
                        responses={responsesByLabel[label]}
                        isCorrect={label === questionData.correctAnswer && changeResponseColor}
                        timeIsUp={timeIsUp}
                    />
                ))}
            </div>
        </div>
    );
};

export default Question;