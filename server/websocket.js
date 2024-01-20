const WebSocket = require('ws');
const server = new WebSocket.Server({ port: 8080 });
const fs = require('fs');
const { json } = require('express');

const questions = JSON.parse(fs.readFileSync('data.json', 'utf8'));

let clientId = 0;
let clients = {};

console.log('Serveur WebSocket lancé sur le port 8080');

server.on('connection', socket => {
  const currentClientId = ++clientId;
  clients[currentClientId] = socket;
  console.log('New client connected with ID:', currentClientId);

  socket.on('message', message => {
    const data = JSON.parse(message.toString());
    let response;

    if (data.type === 'requestQuestion') {
      console.log('question request from client', currentClientId, ':', data);
      const questionData = getRandomQuestion(data.exclude);
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
          tableId: data.tableId,
          isCorrect: isCorrect
      };
    } else if (data.type ==='submitFinal') {
      console.log('Received answer from client', currentClientId, ':', data);
      response = {
        type: 'tableFinished',
        tableId: data.tableId
      }
    } else if (data.type ==='submitFinalTeacher') {
      console.log('Received answer from client', currentClientId, ':', data);
      response = {
        type: 'tableFinishedWithCorrect',
        tableId: data.tableId,
        correct: data.isCorrect
      }
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

function getRandomQuestion(exclude = []) {
  let possibleQuestions = questions.filter(question => !exclude.includes(question.id));
  if (possibleQuestions.length === 0) {
    return null;
  }
  let randomIndex = Math.floor(Math.random() * possibleQuestions.length);
  return possibleQuestions[randomIndex];
}

