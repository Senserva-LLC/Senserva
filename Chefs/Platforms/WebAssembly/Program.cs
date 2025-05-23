using Uno.UI.Hosting;

namespace Simeserva;

public class Program
{
	static async Task Main(string[] args)

	{
		App.InitializeLogging();

		var host = UnoPlatformHostBuilder.Create()
			.App(() => new App())
			.UseWebAssembly()
			.Build();

		await host.RunAsync();
	}
}
