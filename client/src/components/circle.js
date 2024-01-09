import React from 'react';

const Circle = ({ number, isActive, onClick }) => {
    const style = {
        width: '120px',
        height: '120px',
        borderRadius: '50%',
        backgroundColor: isActive ? '#4CAF50' : 'lightgray',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        margin: '10px',
        cursor: 'pointer',
        position: 'relative', // Ajouté pour le positionnement de la pseudo-élément
    };

    const waitingClass = !isActive ? 'waiting' : ''; // Classe pour l'effet d'attente

    return (
        <div style={style} onClick={onClick} className={waitingClass}>
            {number}
        </div>
    );
};

export default Circle;
