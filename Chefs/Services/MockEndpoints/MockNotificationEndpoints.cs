namespace Chefs.Services;

public class MockNotificationEndpoints(string basePath, ISerializer serializer) : BaseMockEndpoint(serializer)
{
	public string HandleNotificationsRequest(HttpRequestMessage request)
	{
		var notifications = LoadData<List<NotificationData>>("Notifications.json")
							?? [];

		//GetRecipesAsync all notifications
		if (request.RequestUri.AbsolutePath == "/api/Notification" && request.Method == HttpMethod.Get)
		{
			return serializer.ToString(notifications);
		}

		return "{}";
	}
}
