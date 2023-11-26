let socket;

document.querySelector('#connect').onclick = () => {
    socket = new WebSocket('ws://localhost:8080');

    socket.onmessage = ({ data }) => {
        console.log('Message from server: ', data);
    };

    socket.onclose = () => {
        console.log('Closed connection with server');
    };

    socket.onopen = () => {
        console.log('Connected to server');
    };
};

document.querySelector('#sendMessage').onclick = () => {
    if (socket && socket.readyState === WebSocket.OPEN) {
        socket.send('Hello!');
    } else {
        console.log('Socket is not open.');
    }
};

document.querySelector('#disconnect').onclick = () => {
    if (socket && socket.readyState === WebSocket.OPEN) {
        socket.close();
    } else {
        console.log('Socket is not open or already closed.');
    }
};