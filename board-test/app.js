let socket;
let currentQuestionId;

let id=2;
let currentTable;

document.querySelector('#connect').onclick = () => {
    socket = new WebSocket('ws://localhost:8080');

    socket.onmessage = ({ data }) => {
        console.log('Feedback from quiz: ', data);
        console.log('Current question ID: ', currentQuestionId);
        const message = JSON.parse(data);
        
        if (message.type === 'question') {
            currentQuestionId = message.question.questionId;
        } else if (message.type === 'answerResult') {
            console.log('Votre réponse est :', message.isCorrect ? 'Correcte' : 'Incorrecte');
        }
    };

    socket.onopen = () => {
        currentTable = id++;
        console.log('Connected to server, table: ' + currentTable);
    };

    socket.onclose = () => {
        console.log('Disconnected from server');
    };
};

const sendAnswer = (answer, id) => {
    if (socket && socket.readyState === WebSocket.OPEN) {
        socket.send(JSON.stringify({ type: 'submitAnswer', answer: answer, questionId: currentQuestionId, tableId: id }));
    } else {
        console.log('Socket is not open.');
    }
};

const sendFinish = (id) => {
    if (socket && socket.readyState === WebSocket.OPEN) {
        socket.send(JSON.stringify({ type: 'submitFinal', tableId: id }));
    } else {
        console.log('Socket is not open.');
    }
};

document.querySelector('#answerA').onclick = () => sendAnswer("A", currentTable);
document.querySelector('#answerB').onclick = () => sendAnswer("B", currentTable);
document.querySelector('#answerC').onclick = () => sendAnswer('C', currentTable);
document.querySelector('#answerD').onclick = () => sendAnswer('D', currentTable);
document.querySelector('#terminer').onclick = () => sendFinish(currentTable);

document.querySelector('#disconnect').onclick = () => {
    if (socket) {
        socket.close();
    }
};