using Microsoft.Extensions.Logging;

namespace FGC.Notifications.Application.Services;

public class NotificationService
{
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(ILogger<NotificationService> logger)
    {
        _logger = logger;
    }

    public Task SendWelcomeAsync(Guid userId, string userName, string userEmail)
    {
        _logger.LogInformation(
            "[NOTIFICAÇÃO] Boas-vindas enviadas | UserId: {UserId} | Nome: {Name} | Email: {Email}",
            userId, userName, userEmail);

        // Em produção: integrar com SendGrid, SES, etc.
        return Task.CompletedTask;
    }

    public Task SendPaymentApprovedAsync(Guid userId, Guid gameId, Guid orderId)
    {
        _logger.LogInformation(
            "[NOTIFICAÇÃO] Pagamento aprovado | UserId: {UserId} | GameId: {GameId} | OrderId: {OrderId}",
            userId, gameId, orderId);

        return Task.CompletedTask;
    }

    public Task SendPaymentRejectedAsync(Guid userId, Guid gameId, Guid orderId)
    {
        _logger.LogInformation(
            "[NOTIFICAÇÃO] Pagamento rejeitado | UserId: {UserId} | GameId: {GameId} | OrderId: {OrderId}",
            userId, gameId, orderId);

        return Task.CompletedTask;
    }
}
