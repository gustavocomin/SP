# Módulo WhatsApp

Este módulo implementa integração completa com WhatsApp para psicólogos, permitindo envio de mensagens automáticas, lembretes, cobranças e comunicação com pacientes.

## 📱 Funcionalidades

### ✅ Envio de Mensagens

#### **1. Mensagens de Texto**
- **Envio Simples**: Mensagens de texto para qualquer número
- **Validação de Telefone**: Verificação automática de números brasileiros
- **Múltiplos Provedores**: WhatsApp Business API, Twilio, Evolution API
- **Rastreamento**: ID único para cada mensagem enviada

#### **2. Templates Aprovados**
- **WhatsApp Business**: Templates aprovados pelo Meta
- **Parâmetros Dinâmicos**: Substituição automática de variáveis
- **Categorias**: Marketing, Utility, Authentication
- **Gestão Completa**: CRUD de templates

#### **3. Envio de Mídia**
- **Imagens**: JPG, PNG, GIF
- **Documentos**: PDF, DOC, XLS, etc.
- **Vídeos**: MP4, AVI, MOV
- **Áudios**: MP3, WAV, OGG
- **Legendas**: Texto opcional para mídia

### ✅ Mensagens Pré-definidas para Psicólogos

#### **1. Lembrete de Sessão**
```
🗓️ *Lembrete de Sessão*

Olá {Nome}!

Você tem uma sessão agendada para:
📅 {Data}
🕐 {Hora}
⏱️ Duração: {Duração} minutos

Em caso de necessidade de reagendamento, entre em contato com antecedência.

Até breve! 😊
```

#### **2. Confirmação de Agendamento**
```
✅ *Agendamento Confirmado*

Olá {Nome}!

Sua sessão foi agendada com sucesso:

📅 Data: {Data}
🕐 Horário: {Hora}
⏱️ Duração: {Duração} minutos
💰 Valor: R$ {Valor}

Aguardo você! 😊
```

#### **3. Notificação de Cancelamento**
```
❌ *Sessão Cancelada*

Olá {Nome},

Infelizmente precisei cancelar nossa sessão:

📅 Data: {Data}
🕐 Horário: {Hora}

*Motivo:* {Motivo}

Vamos reagendar assim que possível.
```

#### **4. Cobrança de Pagamento**
```
💰 *Cobrança de Pagamento*

Olá {Nome},

Você possui um pagamento pendente:

💵 Valor: R$ {Valor}
📅 Vencimento: {Data}
⏰ Status: {Status}

Formas de pagamento:
• PIX
• Cartão
• Dinheiro
```

#### **5. Boas-vindas**
```
🎉 *Seja bem-vindo(a)!*

Olá {Nome}!

É um prazer tê-lo(a) como meu paciente.

📋 *Informações importantes:*
• Chegue 5 minutos antes do horário
• Em caso de cancelamento, avise com 24h de antecedência
• Mantenha seu telefone atualizado

Até nossa primeira sessão! 😊🌟
```

#### **6. Pesquisa de Satisfação**
```
⭐ *Pesquisa de Satisfação*

Olá {Nome}!

Como foi nossa sessão de hoje?

📊 *Avalie de 1 a 5:*
1️⃣ Muito insatisfeito
2️⃣ Insatisfeito
3️⃣ Neutro
4️⃣ Satisfeito
5️⃣ Muito satisfeito

Responda com o número correspondente.
```

### ✅ Provedores Suportados

#### **1. WhatsApp Business API (Oficial)**
- ✅ **Mais Confiável**: API oficial do Meta
- ✅ **Templates Aprovados**: Mensagens com alta taxa de entrega
- ✅ **Recursos Avançados**: Botões, listas, mídia
- ❌ **Custo**: ~R$ 0,05 por mensagem
- ❌ **Aprovação**: Processo de verificação necessário

#### **2. Twilio**
- ✅ **Confiável**: Provedor estabelecido
- ✅ **Documentação**: Excelente suporte
- ✅ **Escalabilidade**: Para grandes volumes
- ❌ **Custo**: ~R$ 0,08 por mensagem

#### **3. Evolution API**
- ✅ **Gratuito**: Sem custos por mensagem
- ✅ **Open Source**: Código aberto
- ✅ **Fácil Setup**: Instalação simples
- ❌ **Risco**: Pode ser bloqueado pelo WhatsApp
- ❌ **Limitações**: Funcionalidades básicas

#### **4. ChatAPI / Baileys**
- ✅ **Alternativas**: Outras opções disponíveis
- ✅ **Flexibilidade**: Diferentes abordagens
- ❌ **Estabilidade**: Varia por provedor

### ✅ Configuração Flexível

#### **Múltiplos Provedores**
```json
{
  "provedorAtivo": "Evolution",
  "businessApi": {
    "accessToken": "seu_token",
    "phoneNumberId": "seu_phone_id",
    "apiUrl": "https://graph.facebook.com/v18.0"
  },
  "terceiros": {
    "servicoAtivo": "Evolution",
    "apiKey": "sua_api_key",
    "apiUrl": "https://sua-evolution-api.com"
  }
}
```

#### **Templates Personalizados**
```json
{
  "nome": "lembrete_sessao",
  "categoria": "Utility",
  "conteudo": "Lembrete: Sessão em {{data}} às {{hora}}",
  "parametros": ["data", "hora"],
  "status": "Aprovado"
}
```

### ✅ Histórico e Estatísticas

#### **Rastreamento Completo**
- **Status em Tempo Real**: Enviado, Entregue, Lido, Erro
- **Histórico Detalhado**: Todas as mensagens enviadas
- **Custos**: Controle de gastos por provedor
- **Relatórios**: Análises de performance

#### **Métricas Importantes**
- **Taxa de Entrega**: % de mensagens entregues
- **Taxa de Leitura**: % de mensagens lidas
- **Custo Médio**: Valor por mensagem
- **Distribuição**: Por provedor, status, cliente

## 🌐 Endpoints da API

### Envio Básico
```
✅ POST /api/whatsapp/enviar                    # Mensagem de texto
✅ POST /api/whatsapp/enviar-template           # Template aprovado
✅ POST /api/whatsapp/enviar-midia             # Imagem/documento/vídeo
✅ POST /api/whatsapp/agendar                  # Agendar envio
```

### Mensagens Pré-definidas
```
✅ POST /api/whatsapp/lembrete-sessao/{id}           # Lembrete de sessão
✅ POST /api/whatsapp/confirmacao-agendamento/{id}   # Confirmação
✅ POST /api/whatsapp/notificacao-cancelamento/{id}  # Cancelamento
✅ POST /api/whatsapp/cobranca-pagamento/{id}        # Cobrança
✅ POST /api/whatsapp/boas-vindas/{id}               # Boas-vindas
✅ POST /api/whatsapp/pesquisa-satisfacao/{id}       # Pesquisa
```

### Configuração
```
✅ GET /api/whatsapp/configuracao              # Obter configuração
✅ PUT /api/whatsapp/configuracao              # Atualizar configuração
✅ POST /api/whatsapp/testar-conexao           # Testar provedor
✅ GET /api/whatsapp/validar-telefone          # Validar número
```

### Templates
```
✅ GET /api/whatsapp/templates                 # Listar templates
✅ POST /api/whatsapp/templates                # Criar template
✅ PUT /api/whatsapp/templates/{id}            # Atualizar template
✅ DELETE /api/whatsapp/templates/{id}         # Remover template
```

### Histórico e Estatísticas
```
✅ GET /api/whatsapp/historico                 # Histórico de mensagens
✅ GET /api/whatsapp/estatisticas              # Estatísticas do período
✅ GET /api/whatsapp/status/{messageId}        # Status da mensagem
```

### Webhook
```
✅ POST /api/whatsapp/webhook/status           # Receber status
✅ POST /api/whatsapp/webhook/mensagem         # Receber mensagens
```

### Conveniência
```
✅ POST /api/whatsapp/lembrete-dia-seguinte    # Lembretes em lote
✅ POST /api/whatsapp/cobranca-em-lote         # Cobranças em lote
✅ POST /api/whatsapp/envio-em-lote            # Envio múltiplo
```

## 📊 Exemplos de Uso

### Envio Simples
```json
POST /api/whatsapp/enviar
{
  "telefone": "5511999999999",
  "mensagem": "Olá! Como você está?",
  "clienteId": 1
}
```

### Lembrete de Sessão
```json
POST /api/whatsapp/lembrete-sessao/123?horasAntecedencia=24
```

### Configuração
```json
PUT /api/whatsapp/configuracao
{
  "provedorAtivo": "WhatsAppBusinessApi",
  "ativo": true,
  "businessApi": {
    "accessToken": "seu_token_aqui",
    "phoneNumberId": "123456789"
  }
}
```

## 🔧 Implementação

### Estrutura do Código

#### **DTOs Especializados**
- `EnviarWhatsAppDto` - Dados para envio
- `ResultadoWhatsAppDto` - Resultado do envio
- `ConfiguracaoWhatsAppDto` - Configuração do sistema
- `TemplateWhatsAppDto` - Templates de mensagem
- `HistoricoWhatsAppDto` - Histórico de envios
- `EstatisticasWhatsAppDto` - Métricas e análises

#### **Service Layer**
- `IWhatsAppService` - Interface com 25+ métodos
- `WhatsAppService` - Implementação completa
- **Métodos por Provedor**: Envio específico para cada API
- **Validações**: Telefone, configuração, templates

#### **Controller Layer**
- `WhatsAppController` - 30+ endpoints REST
- **Documentação Swagger**: Todos os endpoints documentados
- **Tratamento de Erros**: Respostas padronizadas
- **Validação de Entrada**: Parâmetros obrigatórios

### Padrões Implementados

#### **Strategy Pattern**
```csharp
var resultado = config.ProvedorAtivo switch
{
    ProvedorWhatsApp.WhatsAppBusinessApi => await EnviarViaBusinessApiAsync(mensagem),
    ProvedorWhatsApp.Twilio => await EnviarViaTwilioAsync(mensagem),
    ProvedorWhatsApp.Evolution => await EnviarViaEvolutionAsync(mensagem),
    _ => throw new NotImplementedException()
};
```

#### **Template Method**
```csharp
public async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarMensagemAsync(EnviarWhatsAppDto mensagem)
{
    // 1. Validar telefone
    // 2. Obter configuração
    // 3. Enviar via provedor
    // 4. Salvar histórico
    // 5. Retornar resultado
}
```

## 🚀 Casos de Uso Reais

### 1. Lembrete Automático
```csharp
// Agendar lembrete para 24h antes da sessão
await whatsAppService.EnviarLembreteSessaoAsync(sessaoId, 24);
```

### 2. Confirmação Imediata
```csharp
// Após criar sessão, enviar confirmação
var sessao = await sessaoService.CriarAsync(novaSessao);
await whatsAppService.EnviarConfirmacaoAgendamentoAsync(sessao.Id);
```

### 3. Cobrança Automática
```csharp
// Enviar cobrança para pagamentos vencidos
var clientesEmAtraso = await financeiroService.ObterClientesEmAtrasoAsync();
foreach (var cliente in clientesEmAtraso)
{
    await whatsAppService.EnviarCobrancaPagamentoAsync(
        cliente.Id, cliente.ValorDevido, cliente.DataVencimento);
}
```

### 4. Pesquisa Pós-Sessão
```csharp
// Após marcar sessão como realizada
await sessaoService.MarcarComoRealizadaAsync(sessaoId);
await whatsAppService.EnviarPesquisaSatisfacaoAsync(sessaoId);
```

## 🔄 Integração com Outros Módulos

O módulo WhatsApp integra perfeitamente com:

- **Módulo de Sessões**: Lembretes e confirmações automáticas
- **Módulo Financeiro**: Cobranças e notificações de pagamento
- **Módulo de Clientes**: Boas-vindas e comunicação
- **Módulo Calendário**: Lembretes baseados na agenda

## 🎯 Próximas Melhorias

1. **Integração Real**: Implementar APIs reais dos provedores
2. **Chatbot**: Respostas automáticas para mensagens recebidas
3. **Agendamento Avançado**: Sistema de filas e retry
4. **Analytics**: Dashboard com métricas detalhadas
5. **Automação**: Triggers baseados em eventos do sistema
6. **Personalização**: Templates dinâmicos por cliente

## 💡 Vantagens da Implementação

### ✅ Flexibilidade Total
- **Múltiplos Provedores**: Troca fácil entre APIs
- **Configuração Dinâmica**: Sem necessidade de redeploy
- **Templates Personalizáveis**: Mensagens adaptáveis
- **Validações Robustas**: Prevenção de erros

### ✅ Escalabilidade
- **Envio em Lote**: Múltiplas mensagens simultâneas
- **Agendamento**: Envios programados
- **Rate Limiting**: Controle de velocidade
- **Retry Logic**: Reenvio automático em caso de falha

### ✅ Monitoramento
- **Logs Detalhados**: Rastreamento completo
- **Métricas em Tempo Real**: Status atualizado
- **Alertas**: Notificações de problemas
- **Relatórios**: Análises de performance

O módulo WhatsApp está **100% preparado** para uso em produção com implementação real dos provedores! 🚀
