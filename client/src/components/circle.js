import React from 'react';

const Circle = ({ number, isActive }) => {
    const style = {
        width: '150px',
        height: '150px',
        borderRadius: '50%',
        backgroundColor: isActive ? '#4CAF50' : 'lightgray',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        margin: '10px'
    };

    return (
        <div style={style}>
            {number}
        </div>
    );
};

export default Circle;
