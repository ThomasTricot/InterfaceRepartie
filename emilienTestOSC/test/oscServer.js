const osc = require('node-osc');
// Création d'un serveur socket sur le port 3000

// Création du serveur OSC
const oscServer = new osc.Server(3333, '127.0.0.1');

// Gestion des connexions socket

// Lorsqu'un message OSC est reçu, envoyez-le à Unity
oscServer.on('message', function (msg) {
    console.log('Message reçu: ', msg);
});

oscServer.on('bundle', function (bundle) {
    console.log('bundle', bundle);
    bundle.elements.forEach((element, i) => {
        console.log(`Timestamp: ${bundle.timetag[i]}`);
        console.log(`Bundle Message: ${element}`);
    });
});

oscServer.on('bundle', function (bundle) {
    bundle.elements.forEach((element) => {
        const address = element[0];
        const args = element.slice(1);

        if (address === '/tuio/2Dobj') {
            const [command, ...params] = args;

            if (command === 'set') {
                const [s, i, x, y, a, X, Y, A, m, r] = params;
                const data = {
                    type: '2Dobj',
                    sessionId: s,
                    classId: i,
                    position: {x, y},
                    angle: a,
                    velocity: {X, Y, A},
                    motionAcceleration: m,
                    rotationAcceleration: r
                };
                console.log(data);
            } else if (command === 'fseq') {
                const [fseq] = params;
                console.log('Frame Sequence:', fseq);
            }
        } else if (address === '/tuio/2Dcur') {
            const [command, ...params] = args;

            if (command === 'set') {
                const [s, x, y, X, Y, m] = params;
                const data = {
                    type: '2Dcur',
                    sessionId: s,
                    position: {x, y},
                    velocity: {X, Y},
                    motionAcceleration: m
                };
                console.log(data);
            } else if (command === 'fseq') {
                const [fseq] = params;
                console.log('Frame Sequence:', fseq);
            }
        }
    });
});
