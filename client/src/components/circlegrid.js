import React, { useState } from 'react';
import Circle from './circle.js';

const CirclesGrid = ({ activeCircles }) => {
    const circleWidth = 150;
    const margin = 40;
    const containerWidth = 2 * circleWidth + margin;

    return (
        <div style={{ display: 'flex', flexDirection: 'row', flexWrap: 'wrap', width: `${containerWidth}px`, fontSize: '40px', fontWeight: 'bold' }}>
            {[1, 2, 3, 4].map((number) => (
                <Circle key={number} number={number} isActive={activeCircles.includes(number)} />
            ))}
            
        </div>
    );
};

export default CirclesGrid;
