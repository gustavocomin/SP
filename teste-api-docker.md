# Teste da API Docker

## Status dos Containers
```bash
docker-compose ps
```

## Teste Health Check
```bash
curl -X GET "http://localhost:7169/health"
```

## Teste Swagger
Abrir no navegador: http://localhost:7169/swagger

## Teste Criar Cliente (JSON simples)
```bash
curl -X POST "http://localhost:7169/api/clientes" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "email": "joao@email.com", 
    "telefone": "48999887766",
    "cpf": "12345678901",
    "dataNascimento": "1990-01-01T00:00:00",
    "estado": "SC",
    "cidade": "Florianópolis", 
    "cep": "88000000",
    "endereco": "Rua das Flores",
    "bairro": "Centro",
    "numero": "123",
    "valorSessao": 150.00,
    "formaPagamentoPreferida": "PIX",
    "diaVencimento": 5,
    "aceiteLgpd": true
  }'
```

## Teste Listar Clientes
```bash
curl -X GET "http://localhost:7169/api/clientes"
```

## Logs dos Containers
```bash
# API
docker-compose logs sp-api

# PostgreSQL
docker-compose logs postgres

# Evolution API
docker-compose logs evolution-api
```
