const WebSocket = require('ws');
const server = new WebSocket.Server({ port: 8080 });

server.on('connection', socket => {
  console.log('New client connected!');

  const questionData = {
    question: "Quelle est la capitale de la France ?",
    answers: {
      A: "Paris",
      B: "Lyon",
      C: "Marseille",
      D: "Toulouse"
    }
  };

  socket.send(JSON.stringify(questionData));

  socket.on('message', message => {
    
    let isCorrect = false;
    const correctAnswer = "A";

    const receivedMessage = message.toString();
    console.log(receivedMessage);

    if (receivedMessage === correctAnswer) {
      isCorrect = true;
    }

    const response = {
      answer: receivedMessage,
      isCorrect: isCorrect
    };

    socket.send(JSON.stringify(response));
  });

  socket.onclose = () => {
    console.log('Client disconnected');
  };
});
