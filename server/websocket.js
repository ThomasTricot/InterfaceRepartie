const WebSocket = require('ws');
const server = new WebSocket.Server({ port: 8080 });

server.on('connection', socket => {

  console.log('New client connected!');
  socket.send('Connexion with server on port 8080');

  socket.on('message', message => {
    console.log(`Received message => ${message}`);
    socket.send(`Server received this message and sent you back: ${message}`);
  });

  socket.onclose = () => {
    console.log('Client disconnected');
  };

});