const express = require('express');
const http = require('http');

const app = express();
app.use(express.json());

app.get('/api/data/test', (req, res) => {
    res.send('Hello world!');
});

app.post('/api/data', (req, res) => {
});

const server = http.createServer(app);

const PORT = process.env.PORT || 3000;
server.listen(PORT, () => console.log(`Server running on port ${PORT}`));
