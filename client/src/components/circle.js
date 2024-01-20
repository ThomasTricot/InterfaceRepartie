import React from 'react';

const Circle = ({ number, studentValidation, teacherValidation }) => {
    const style = {
        width: '120px',
        height: '120px',
        borderRadius: '50%',
        backgroundColor: teacherValidation === true ? '#4CAF50' : (teacherValidation === false ? 'lightgray' : (studentValidation ? 'orange' : 'lightgray')),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        justifyContent: 'center',
        margin: '10px',
        cursor: 'default',
        position: 'relative',
    };

    const numberStyle = {
        position: 'absolute',
        top: '-7%',
        left: '50%',
        transform: 'translateX(-50%)',
        fontSize: '1.1em',
        fontWeight: 'lighter',
        zIndex: 2,
    };

    const imgStyle = {
        width: '70%',
        marginTop: '30%',
    };

    const waitingClass = !teacherValidation ? 'waiting' : '';

    return (
        <div style={style} className={waitingClass}>
            <div style={numberStyle}>{number}</div>
            <img src="table.png" alt="Table" style={imgStyle} />
        </div>
    );
};

export default Circle;
