const WebSocket = require('ws');
const server = new WebSocket.Server({ port: 8080 });
let clientId = 0;
let clients = {};

server.on('connection', socket => {
  const currentClientId = ++clientId;
  clients[currentClientId] = socket;
  console.log('New client connected with ID:', currentClientId);

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

    Object.keys(clients).forEach(id => {
      clients[id].send(JSON.stringify(response));
      console.log(id);
    });
  
  });

  socket.onclose = () => {
    delete clients[currentClientId];
    console.log('Client disconnected', currentClientId);
  };
});
