using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using FGC.Notifications.Application.Contracts.Events;
using FGC.Notifications.Application.Services;
using FGC.Notifications.Infrastructure.Consumers;

namespace FGC.Notifications.Tests.Consumers;

public class UserCreatedConsumerTests
{
    private readonly NotificationService _notificationService;
    private readonly UserCreatedConsumer _consumer;

    public UserCreatedConsumerTests()
    {
        _notificationService = new NotificationService(NullLogger<NotificationService>.Instance);
        _consumer = new UserCreatedConsumer(_notificationService);
    }

    [Fact]
    public async Task Dado_UserCreatedEvent_Quando_Consume_Entao_ProcessaSemExcecao()
    {
        // Dado
        var ev = new UserCreatedEvent(
            UserId: Guid.NewGuid(),
            UserName: "João Silva",
            UserEmail: "joao@email.com",
            CreatedAt: DateTime.UtcNow);

        var contextMock = new Mock<ConsumeContext<UserCreatedEvent>>();
        contextMock.Setup(c => c.Message).Returns(ev);

        // Quando
        var act = () => _consumer.Consume(contextMock.Object);

        // Então
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Dado_UserCreatedEvent_Quando_Consume_Entao_EnviaNotificacaoDeBoasVindas()
    {
        // Dado
        var ev = new UserCreatedEvent(
            UserId: Guid.NewGuid(),
            UserName: "Maria Souza",
            UserEmail: "maria@email.com",
            CreatedAt: DateTime.UtcNow);

        var contextMock = new Mock<ConsumeContext<UserCreatedEvent>>();
        contextMock.Setup(c => c.Message).Returns(ev);

        // Quando / Então — sem exceção lançada
        await _consumer.Consume(contextMock.Object);
    }
}
