import React, { useState } from 'react';
import Circle from './circle.js';

const CirclesGrid = ({ studentCircles, teacherCircles }) => {
    const circleWidth = 140;
    const containerWidth = 4 * circleWidth;

    return (
        <div style={{ display: 'flex', flexDirection: 'row', flexWrap: 'wrap', width: `${containerWidth}px`, fontSize: '40px', fontWeight: 'bold' }}>
            {[1, 2, 3, 4].map((number) => (
                <Circle key={number} number={number} studentValidation={studentCircles.includes(number)} teacherValidation={teacherCircles[number]} />
            ))}
        </div>
    );
};

export default CirclesGrid;
