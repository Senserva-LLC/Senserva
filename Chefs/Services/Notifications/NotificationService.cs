
namespace Simeserva.Services.Notifications;

public class NotificationService() : INotificationService
{
	public async ValueTask<IImmutableList<Notification>> GetAll(CancellationToken ct)
	{
		return Get();
	}

	internal IImmutableList<Notification> Get() => new List<Notification>().ToImmutableList();

}
