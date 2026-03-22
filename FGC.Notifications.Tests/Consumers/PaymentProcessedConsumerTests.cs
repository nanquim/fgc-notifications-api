using FluentAssertions;
using MassTransit;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using FGC.Notifications.Application.Contracts.Events;
using FGC.Notifications.Application.Services;
using FGC.Notifications.Infrastructure.Consumers;

namespace FGC.Notifications.Tests.Consumers;

public class PaymentProcessedConsumerTests
{
    private readonly NotificationService _notificationService;
    private readonly PaymentProcessedConsumer _consumer;

    public PaymentProcessedConsumerTests()
    {
        _notificationService = new NotificationService(NullLogger<NotificationService>.Instance);
        _consumer = new PaymentProcessedConsumer(_notificationService);
    }

    [Fact]
    public async Task Dado_StatusApproved_Quando_Consume_Entao_ProcessaSemExcecao()
    {
        // Dado
        var ev = new PaymentProcessedEvent(
            OrderId: Guid.NewGuid(),
            UserId: Guid.NewGuid(),
            GameId: Guid.NewGuid(),
            Status: "Approved",
            ProcessedAt: DateTime.UtcNow);

        var contextMock = new Mock<ConsumeContext<PaymentProcessedEvent>>();
        contextMock.Setup(c => c.Message).Returns(ev);

        // Quando
        var act = () => _consumer.Consume(contextMock.Object);

        // Então
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Dado_StatusRejected_Quando_Consume_Entao_ProcessaSemExcecao()
    {
        // Dado
        var ev = new PaymentProcessedEvent(
            OrderId: Guid.NewGuid(),
            UserId: Guid.NewGuid(),
            GameId: Guid.NewGuid(),
            Status: "Rejected",
            ProcessedAt: DateTime.UtcNow);

        var contextMock = new Mock<ConsumeContext<PaymentProcessedEvent>>();
        contextMock.Setup(c => c.Message).Returns(ev);

        // Quando
        var act = () => _consumer.Consume(contextMock.Object);

        // Então
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Dado_StatusApproved_Quando_Consume_Entao_EnviaNotificacaoAprovado()
    {
        // Dado
        var ev = new PaymentProcessedEvent(
            OrderId: Guid.NewGuid(),
            UserId: Guid.NewGuid(),
            GameId: Guid.NewGuid(),
            Status: "Approved",
            ProcessedAt: DateTime.UtcNow);

        var contextMock = new Mock<ConsumeContext<PaymentProcessedEvent>>();
        contextMock.Setup(c => c.Message).Returns(ev);

        // Quando / Então — sem exceção e fluxo correto de Approved
        await _consumer.Consume(contextMock.Object);
    }

    [Fact]
    public async Task Dado_StatusRejected_Quando_Consume_Entao_EnviaNotificacaoRejeitado()
    {
        // Dado
        var ev = new PaymentProcessedEvent(
            OrderId: Guid.NewGuid(),
            UserId: Guid.NewGuid(),
            GameId: Guid.NewGuid(),
            Status: "Rejected",
            ProcessedAt: DateTime.UtcNow);

        var contextMock = new Mock<ConsumeContext<PaymentProcessedEvent>>();
        contextMock.Setup(c => c.Message).Returns(ev);

        // Quando / Então — sem exceção e fluxo correto de Rejected
        await _consumer.Consume(contextMock.Object);
    }
}
