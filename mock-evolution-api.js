const http = require('http');
const url = require('url');
const port = 8080;

// Função para parsear JSON do body
function parseBody(req) {
    return new Promise((resolve, reject) => {
        let body = '';
        req.on('data', chunk => {
            body += chunk.toString();
        });
        req.on('end', () => {
            try {
                resolve(body ? JSON.parse(body) : {});
            } catch (err) {
                resolve({});
            }
        });
    });
}

// Função para enviar resposta JSON
function sendJSON(res, data, statusCode = 200) {
    res.writeHead(statusCode, {
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
        'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
        'Access-Control-Allow-Headers': 'Content-Type, Authorization, apikey'
    });
    res.end(JSON.stringify(data));
}

// Criar servidor
const server = http.createServer(async (req, res) => {
    const parsedUrl = url.parse(req.url, true);
    const path = parsedUrl.pathname;
    const method = req.method;

    console.log(`${new Date().toISOString()} - ${method} ${path}`);
    console.log('Headers:', req.headers);

    // Handle CORS preflight
    if (method === 'OPTIONS') {
        sendJSON(res, { success: true });
        return;
    }

    try {
        const body = await parseBody(req);
        if (Object.keys(body).length > 0) {
            console.log('Body:', JSON.stringify(body, null, 2));
        }

        // Rotas
        if (method === 'POST' && path === '/instance/create') {
            console.log('🔧 Criando instância:', body);
            sendJSON(res, {
                success: true,
                message: 'Instância criada com sucesso',
                instance: body.instanceName || 'psicologo'
            });
        }
        else if (method === 'GET' && path.startsWith('/instance/connect/')) {
            const instanceName = path.split('/')[3];
            console.log('📱 Gerando QR Code para instância:', instanceName);
            sendJSON(res, {
                success: true,
                message: 'QR Code gerado',
                qrcode: 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==',
                instanceName: instanceName
            });
        }
        else if (method === 'POST' && path.startsWith('/message/sendText/')) {
            const instanceName = path.split('/')[3];
            const { number, text } = body;

            console.log('📤 Enviando mensagem:');
            console.log('  Instância:', instanceName);
            console.log('  Número:', number);
            console.log('  Texto:', text);

            sendJSON(res, {
                success: true,
                message: 'Mensagem enviada com sucesso',
                key: {
                    id: `msg_${Date.now()}_${Math.random().toString(36).substr(2, 9)}`,
                    remoteJid: `${number}@s.whatsapp.net`,
                    fromMe: true
                },
                messageId: `evolution_${Date.now()}`,
                status: 'sent'
            });
        }
        else if (method === 'GET' && path.startsWith('/instance/status/')) {
            const instanceName = path.split('/')[3];
            console.log('📊 Status da instância:', instanceName);
            sendJSON(res, {
                success: true,
                instance: instanceName,
                status: 'connected',
                qrcode: null
            });
        }
        else if (method === 'GET' && path === '/health') {
            sendJSON(res, {
                success: true,
                message: 'Evolution API Mock está funcionando!',
                timestamp: new Date().toISOString()
            });
        }
        else {
            sendJSON(res, {
                success: false,
                message: 'Endpoint não encontrado',
                path: path,
                method: method
            }, 404);
        }
    } catch (err) {
        console.error('❌ Erro:', err);
        sendJSON(res, {
            success: false,
            message: 'Erro interno do servidor',
            error: err.message
        }, 500);
    }
});

// Iniciar servidor
server.listen(port, () => {
    console.log('🚀 Evolution API Mock rodando em http://localhost:' + port);
    console.log('📋 Endpoints disponíveis:');
    console.log('  POST /instance/create');
    console.log('  GET  /instance/connect/:instanceName');
    console.log('  POST /message/sendText/:instanceName');
    console.log('  GET  /instance/status/:instanceName');
    console.log('  GET  /health');
    console.log('');
    console.log('🔑 Use a API Key: sua-chave-super-secreta-aqui-123456');
    console.log('');
});
