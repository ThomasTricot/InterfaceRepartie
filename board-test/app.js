let socket;
let currentQuestionId;

let currentTable;

document.querySelector('#connect').onclick = () => {
    currentTable = document.querySelector('#tableId').value;

    socket = new WebSocket('ws://192.168.1.20:8080');

    socket.onmessage = ({ data }) => {
        console.log('Feedback from quiz: ', data);
        const message = JSON.parse(data);

        if (message.type === 'question') {
            currentQuestionId = message.question.questionId;
        } else if (message.type === 'answerResult') {
            console.log('Votre réponse est :', message.isCorrect ? 'Correcte' : 'Incorrecte');
        }
    };

    socket.onopen = () => {
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

const sendFinishTeacher = (id, correct) => {
    if (socket && socket.readyState === WebSocket.OPEN) {
        console.log(JSON.stringify({ type: 'submitFinalTeacher', tableId: id, isCorrect: correct }));
        socket.send(JSON.stringify({ type: 'submitFinalTeacher', tableId: id, isCorrect: correct }));
    } else {
        console.log('Socket is not open.');
    }
};

document.querySelector('#answerA').onclick = () => sendAnswer("A", currentTable);
document.querySelector('#answerB').onclick = () => sendAnswer("B", currentTable);
document.querySelector('#answerC').onclick = () => sendAnswer('C', currentTable);
document.querySelector('#answerD').onclick = () => sendAnswer('D', currentTable);
document.querySelector('#studentFinish').onclick = () => sendFinish(currentTable);
document.querySelector('#teacherOk').onclick = () => sendFinishTeacher(currentTable, true);
document.querySelector('#teacherNotOk').onclick = () => sendFinishTeacher(currentTable, false);

document.querySelector('#disconnect').onclick = () => {
    if (socket) {
        socket.close();
    }
};
