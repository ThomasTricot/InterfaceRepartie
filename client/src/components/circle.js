import React from 'react';

const Circle = ({ number, studentValidation, teacherValidation, onClick }) => {
    const style = {
        width: '120px',
        height: '120px',
        borderRadius: '50%',
        backgroundColor: teacherValidation === true ? '#4CAF50' : (teacherValidation === false ? 'lightgray' : (studentValidation ? 'orange' : 'lightgray')),
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        margin: '10px',
        cursor: 'pointer',
        position: 'relative', // Ajouté pour le positionnement de la pseudo-élément
    };

    const waitingClass = !teacherValidation ? 'waiting' : '';

    return (
        <div style={style} onClick={onClick} className={waitingClass}>
            {number}
        </div>
    );
};

export default Circle;
