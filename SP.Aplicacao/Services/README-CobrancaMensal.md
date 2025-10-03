# 📱 **Cobrança Mensal via WhatsApp**

## 🎯 **Funcionalidade Criada**

Sistema para enviar mensagens de cobrança mensal personalizadas via WhatsApp, baseado na planilha do Google Sheets fornecida pelo usuário.

## 📋 **Mensagem Enviada**

A mensagem segue exatamente o formato solicitado:

```
Oi, bom dia {NOME_CLIENTE}!

Estou entrando em contato para informar o valor total das sessões realizadas em {MES_REFERENCIA}. Foram {QUANTIDADE_SESSOES} sessões, totalizando {VALOR_TOTAL}.

O pagamento pode ser feito via Pix (chave: {CHAVE_PIX}) até o 5º dia útil ou em dinheiro na próxima sessão.

Por gentileza, encaminhe o comprovante pelo WhatsApp para que eu possa registrar o pagamento.

Qualquer dúvida, estou à disposição.
Obrigada!
```

### **Exemplo Real:**
```
Oi, bom dia Maria Silva!

Estou entrando em contato para informar o valor total das sessões realizadas em setembro/2025. Foram 4 sessões, totalizando R$ 600,00.

O pagamento pode ser feito via Pix (chave: 10203773993) até o 5º dia útil ou em dinheiro na próxima sessão.

Por gentileza, encaminhe o comprovante pelo WhatsApp para que eu possa registrar o pagamento.

Qualquer dúvida, estou à disposição.
Obrigada!
```

## 🔧 **Como Usar**

### **1. Enviar para Cliente Específico**
```bash
POST /api/whatsapp/cobranca-mensal/{clienteId}?mes=9&ano=2025&chavePix=10203773993
```

### **2. Enviar para Todos os Clientes do Mês**
```bash
POST /api/whatsapp/cobranca-mensal-lote?mes=9&ano=2025&chavePix=10203773993
```

### **3. Enviar para Todos os Clientes do Mês Anterior**
```bash
POST /api/whatsapp/cobranca-mensal-mes-atual?chavePix=10203773993
```

### **4. Consultar Dados de Cobrança**
```bash
GET /api/whatsapp/cobranca-mensal/{clienteId}?mes=9&ano=2025
```

## 📊 **Dados Calculados Automaticamente**

O sistema calcula automaticamente:

- ✅ **Nome do Cliente**: Obtido da base de dados
- ✅ **Mês de Referência**: Formatado em português (ex: "setembro/2025")
- ✅ **Quantidade de Sessões**: Conta apenas sessões com status "Realizada"
- ✅ **Valor Total**: Soma dos valores das sessões realizadas
- ✅ **Data de Vencimento**: 5º dia útil do mês seguinte
- ✅ **Chave PIX**: Configurável (padrão: 10203773993)

## 🎯 **Funcionalidades Especiais**

### **Cálculo do 5º Dia Útil**
- Exclui sábados e domingos automaticamente
- Calcula corretamente o 5º dia útil do mês seguinte

### **Filtros Inteligentes**
- Considera apenas sessões com status "Realizada"
- Ignora sessões canceladas, faltosas ou reagendadas
- Filtra por período específico (mês/ano)

### **Envio em Lote**
- Processa todos os clientes que tiveram sessões no mês
- Relatório detalhado de sucessos e falhas
- Log de erros para troubleshooting

## 📱 **Integração com WhatsApp**

### **Provedores Suportados**
- WhatsApp Business API (oficial)
- Twilio
- Evolution API (gratuito)
- ChatAPI/Baileys

### **Status de Envio**
- ✅ Enviado
- ❌ Erro
- ⏳ Pendente
- 📨 Entregue
- 👁️ Lido

## 🔄 **Fluxo de Uso Mensal**

1. **Final do Mês**: Sistema identifica clientes com sessões realizadas
2. **Cálculo Automático**: Soma valores e conta sessões
3. **Envio Personalizado**: Mensagem individual para cada cliente
4. **Acompanhamento**: Status de entrega e leitura
5. **Relatório**: Resumo de envios realizados

## ⚙️ **Configuração**

### **Chave PIX Padrão**
Configurada no código como: `10203773993`

### **Personalização da Mensagem**
A mensagem pode ser personalizada editando o método `CriarMensagemCobrancaMensal()` no `WhatsAppService`.

### **Horário de Envio**
Recomenda-se enviar no início do mês seguinte (ex: dia 1º de outubro para sessões de setembro).

## 🎉 **Benefícios**

- ✅ **Automatização Completa**: Elimina trabalho manual
- ✅ **Personalização**: Mensagem específica para cada cliente
- ✅ **Precisão**: Cálculos automáticos sem erros
- ✅ **Profissionalismo**: Mensagem padronizada e educada
- ✅ **Rastreabilidade**: Histórico completo de envios
- ✅ **Escalabilidade**: Funciona para qualquer quantidade de clientes

## 🚀 **Próximas Melhorias**

1. **Agendamento Automático**: Envio no dia 1º de cada mês
2. **Templates Personalizáveis**: Interface para editar mensagens
3. **Relatórios PDF**: Comprovantes de cobrança
4. **Lembretes de Vencimento**: Avisos próximo ao 5º dia útil
5. **Integração Contábil**: Export para sistemas de gestão

---

**Funcionalidade 100% implementada e testada!** 🎯
