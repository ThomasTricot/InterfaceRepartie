let socket;

document.querySelector('#connect').onclick = () => {
    socket = new WebSocket('ws://localhost:8080');

    socket.onmessage = ({ data }) => {
        console.log('Feedback from quiz: ', data); // Affiche si la réponse est correcte ou non
    };

    socket.onopen = () => {
        console.log('Connected to server');
    };

    socket.onclose = () => {
        console.log('Disconnected from server');
    };
};

const sendAnswer = (answer) => {
    if (socket && socket.readyState === WebSocket.OPEN) {
        socket.send(answer);
    } else {
        console.log('Socket is not open.');
    }
};

document.querySelector('#answerA').onclick = () => sendAnswer("A");
document.querySelector('#answerB').onclick = () => sendAnswer("B");
document.querySelector('#answerC').onclick = () => sendAnswer('C');
document.querySelector('#answerD').onclick = () => sendAnswer('D');

document.querySelector('#disconnect').onclick = () => {
    if (socket) {
        socket.close();
    }
};