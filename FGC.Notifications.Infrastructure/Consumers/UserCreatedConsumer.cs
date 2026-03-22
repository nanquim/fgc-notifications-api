using MassTransit;
using FGC.Notifications.Application.Contracts.Events;
using FGC.Notifications.Application.Services;

namespace FGC.Notifications.Infrastructure.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
{
    private readonly NotificationService _notificationService;

    public UserCreatedConsumer(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var ev = context.Message;
        await _notificationService.SendWelcomeAsync(ev.UserId, ev.UserName, ev.UserEmail);
    }
}
