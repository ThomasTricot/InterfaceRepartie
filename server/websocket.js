const WebSocket = require('ws');
const server = new WebSocket.Server({ port: 8080 });
const fs = require('fs');
const { json } = require('express');

const questions = JSON.parse(fs.readFileSync('data.json', 'utf8'));

let clientId = 0;
let clients = {};

server.on('connection', socket => {
  const currentClientId = ++clientId;
  clients[currentClientId] = socket;
  console.log('New client connected with ID:', currentClientId);

  socket.on('message', message => {
    const data = JSON.parse(message.toString());
    let response;

    if (data.type === 'requestQuestion') {
      const questionData = getRandomQuestion();
      response = {
        type: 'question',
        question: questionData,
      };
    } else if (data.type === 'submitAnswer') {
        console.log('Received answer from client', currentClientId, ':', data);
        const isCorrect = data.questionId ? (data.answer === questions[data.questionId].correctAnswer) : false;
        response = {
          type: 'answerResult',
          answer: data.answer,
          isCorrect: isCorrect
      };
    }

    Object.keys(clients).forEach(id => {
      clients[id].send(JSON.stringify(response));
    });
});

  socket.onclose = () => {
    delete clients[currentClientId];
    console.log('Client disconnected', currentClientId);
  };
});

function getRandomQuestion() {
  const randomIndex = Math.floor(Math.random() * questions.length);
  return {
    questionId: randomIndex,
    ...questions[randomIndex]
  };
}
