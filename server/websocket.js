const WebSocket = require('ws');

function initializeWebSocket(server) {
  const wss = new WebSocket.Server({ server });

  wss.on('connection', (ws) => {
    ws.on('message', (message) => {
      console.log('Received message:', message);
    });

    ws.send('Connexion WebSocket établie');
  });
}

module.exports = { initializeWebSocket };
