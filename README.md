# FGC Notifications API

Microsserviço responsável pelo envio de notificações da plataforma **FIAP Cloud Games**.

## Responsabilidades

- Consome `UserCreatedEvent` → envia notificação de boas-vindas
- Consome `PaymentProcessedEvent` → envia confirmação ou rejeição de pagamento

Não possui banco de dados próprio. As notificações são registradas via log (pronto para integração com e-mail, SMS, push, etc.).

## Fluxo de eventos

```
UsersAPI    → [UserCreatedEvent]      → NotificationsAPI → boas-vindas
PaymentsAPI → [PaymentProcessedEvent] → NotificationsAPI → confirmação de pagamento
```

## Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/health` | Health check do serviço |

## Eventos consumidos

| Evento | Origem | Ação |
|--------|--------|------|
| `UserCreatedEvent` | UsersAPI | Envia boas-vindas ao novo usuário |
| `PaymentProcessedEvent` | PaymentsAPI | Envia confirmação (aprovado ou rejeitado) |

## Variáveis de ambiente

| Variável | Descrição | Exemplo |
|----------|-----------|---------|
| `RabbitMQ__Host` | Host do RabbitMQ | `localhost` |
| `RabbitMQ__Username` | Usuário RabbitMQ | `guest` |
| `RabbitMQ__Password` | Senha RabbitMQ | `guest` |

## Executar localmente

```bash
dotnet run --project FGC.Notifications.Api
```

## Executar com Docker

```bash
docker compose up --build
```

API disponível em `http://localhost:5003`.

## Arquitetura

```
FGC.Notifications.Api            → Program.cs, Middlewares, health endpoint
FGC.Notifications.Application    → NotificationService, Contracts/Events
FGC.Notifications.Infrastructure → UserCreatedConsumer, PaymentProcessedConsumer
```
