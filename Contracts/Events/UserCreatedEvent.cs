namespace FGC.Notifications.Application.Contracts.Events;

public record UserCreatedEvent(
    Guid UserId,
    string UserName,
    string UserEmail,
    DateTime CreatedAt
);
