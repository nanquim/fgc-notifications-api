using MassTransit;
using FGC.Notifications.Application.Contracts.Events;
using FGC.Notifications.Application.Services;

namespace FGC.Notifications.Infrastructure.Consumers;

public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
{
    private readonly NotificationService _notificationService;

    public PaymentProcessedConsumer(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        var ev = context.Message;

        if (ev.Status == "Approved")
            await _notificationService.SendPaymentApprovedAsync(ev.UserId, ev.GameId, ev.OrderId);
        else
            await _notificationService.SendPaymentRejectedAsync(ev.UserId, ev.GameId, ev.OrderId);
    }
}
