using Simeserva.Services.LiveData;
using Simeserva.Services.Settings;
using Simeserva.Views.Flyouts;
using Microsoft.Extensions.Configuration;
using Siemserva.Views;

namespace Simeserva;

/*
 * https://www.wallarm.com/what/what-is-json-rpc
 * note AOT https://github.com/microsoft/vs-streamjsonrpc/issues/746#issuecomment-1984713686  can we put in its own DLL, not AOT and then use it?
 *
 * AI:
 * JSON-RPC and REST are two API protocols, but they differ in their approach and how they handle communication between a client and server. REST is a broader architectural style that focuses on resource manipulation using standard HTTP methods, while JSON-RPC is a more specific protocol for remote procedure calls using JSON. [1, 2]
Here's a more detailed breakdown:
REST (Representational State Transfer):

• Resource-oriented: REST treats data as resources, and APIs operate on these resources using standard HTTP methods like GET, POST, PUT, DELETE, etc. [3, 4]
• Stateless: Each request from a client should contain all the information needed to process it, and the server doesn't store any information about previous client interactions. [5]
• Flexibility: REST allows for a wide variety of implementations and can be used with different data formats (JSON, XML, etc.). [1, 5]
• Public APIs: REST is often preferred for public APIs, where scalability, caching, and a uniform interface are important. [3, 6]

JSON-RPC:

• Remote procedure call: JSON-RPC focuses on invoking procedures (functions) on a remote server. [2]
• Specific format: JSON-RPC uses a specific JSON format for communication between client and server. [2]
• Simpler for specific tasks: JSON-RPC can be simpler and easier to consume for straightforward use cases that fit its scope. [1, 7]
• Internal microservices: JSON-RPC is often chosen for internal microservices communication where efficiency and speed are critical. [6]

Key Differences:

| Feature | REST | JSON-RPC  |
| --- | --- | --- |
| Focus | Resource manipulation | Remote procedure calls  |
| Communication | Standard HTTP methods (GET, POST, etc.) | Specific JSON format  |
| State | Stateless | Can be stateless or stateful  |
| Flexibility | High | Lower  |
| Use Cases | Public APIs, web services | Internal microservices, specific tasks  |

When to choose which:

• REST: When you need a flexible, scalable, and resource-oriented approach for public APIs or web services. [3, 6]
• JSON-RPC: When you need a simpler, more direct way to call functions on a remote server, especially in internal microservices or for specific tasks. [1, 6]

AI responses may include mistakes.

[1] https://www.mertech.com/blog/know-your-api-protocols[2] https://www.wallarm.com/what/what-is-json-rpc[3] https://stackoverflow.com/questions/15056878/rest-vs-json-rpc[4] https://www.reddit.com/r/computerscience/comments/j4djbh/rpc_vs_rest/[5] https://aws.amazon.com/compare/the-difference-between-rpc-and-rest/[6] https://medium.com/@apurvaagrawal_95485/rest-vs-rpc-d59c5d13f380[7] https://medium.com/@michejin/what-is-json-rpc-761445136d7c[8] https://www.ankr.com/blog/rpc-vs-rest/[9] https://www.ankr.com/blog/what-is-json-rpc-and-what-is-used-for/[10] https://opsi.org/en/blog/opsi-cli-jsonrpc/[11] https://www.youtube.com/watch?v=DqFMhOTAEeA[12] https://www.youtube.com/watch?v=lOeSDdi8OmM[13] https://medium.com/nerd-for-tech/rest-versus-rpcs-fb1a7d2575bf[14] https://www.ottia.com/en/post/api-architectural-styles

 */

public partial class App : Application
{
	private void ConfigureAppBuilder(IApplicationBuilder builder)
	{
		builder
			// Add navigation support for toolkit controls such as TabBar and NavigationView
			.UseToolkitNavigation()
			.Configure(host => host
				.UseAuthentication(auth =>
					auth.AddCustom(custom =>
					{
						custom
							.Login(async (sp, dispatcher, credentials, cancellationToken) => await ProcessCredentials(credentials));
					}, name: "CustomAuth")
				)
#if DEBUG
				// Switch to Development environment when running in DEBUG
				.UseEnvironment(Environments.Development)
#endif
				// Temporary until uno loggig is fixed in extensions.
				//.UseLogging(configure: (context, logBuilder) =>
				//{
				//	// Configure log levels for different categories of logging
				//	logBuilder.SetMinimumLevel(
				//		context.HostingEnvironment.IsDevelopment() ? LogLevel.Information : LogLevel.Warning);
				//}, enableUnoLogging: true)
				.UseConfiguration(configure: configBuilder =>
					configBuilder
						.EmbeddedSource<App>()
						.Section<AppConfig>()
						.Section<Credentials>()
						.Section<SearchHistory>()
				)

				// Enable localization (see appsettings.json for supported languages)
				.UseLocalization()

				// Register Json serializers (ISerializer)
				.UseSerialization(configure: ConfigureSerialization)
				.ConfigureServices((context, services) =>
				{
					services
						.AddSingleton<ICookbookService, CookbookService>()
						.AddSingleton<IMessenger, WeakReferenceMessenger>()
						.AddSingleton<INotificationService, NotificationService>()
						.AddSingleton<ITechniqueService, TechniqueService>()
						.AddSingleton<ISettingsService, SettingsService>()
						.AddSingleton<IReportsService, ReportsService>()
						.AddSingleton<ICommandsService, CommandsService>()
						.AddSingleton<ITechniqueService, TechniqueService>()
						.AddSingleton<IUserService, UserService>()
						.AddSingleton<ILiveDataService, LiveDataService>();
				})
				.ConfigureAppConfiguration(config =>
				{
					// Clear any launchurl to make sure we always start at beginning
					// Deeplinking issue https://github.com/unoplatform/uno.chefs/issues/738
					var appsettingsPrefix = new Dictionary<string, string?>
							{
								{ HostingConstants.LaunchUrlKey, "" }
							};
					config.AddInMemoryCollection(appsettingsPrefix);
				})
				.UseNavigation(Siemserva.ReactiveViewModelMappings.ViewModelMappings, RegisterRoutes,
					configure: navConfig => navConfig with { AddressBarUpdateEnabled = true },
					configureServices: ConfigureNavServices));
	}

	private async ValueTask<IDictionary<string, string>?> ProcessCredentials(IDictionary<string, string> credentials)
	{
		// Check for username to simulate credential processing
		if (!(credentials?.TryGetValue("Username", out var username) ??
				false && !string.IsNullOrEmpty(username)))
		{
			return null;
		}

		// Simulate successful authentication by creating a dummy token dictionary
		var tokenDictionary = new Dictionary<string, string>
		{
			{ TokenCacheExtensions.AccessTokenKey, "SampleToken" },
			{ TokenCacheExtensions.RefreshTokenKey, "RefreshToken" },
			{ "Expiry", DateTime.Now.AddMinutes(5).ToString("g") } // Set token expiry
		};

		return tokenDictionary;
	}

	private void ConfigureSerialization(HostBuilderContext context, IServiceCollection services)
	{
		services
			.AddJsonTypeInfo(AppConfigContext.Default.AppConfig)
			.AddJsonTypeInfo(AppConfigContext.Default.DictionaryStringAppConfig)
			.AddJsonTypeInfo(AppConfigContext.Default.String);
	}

	private void ConfigureNavServices(HostBuilderContext context, IServiceCollection services)
	{
		services.AddTransient<Flyout, ResponsiveDrawerFlyout>();
	}

	private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
	{
		views.Register(
			new ViewMap(ViewModel: typeof(ShellModel)),
			new ViewMap<MainPage, MainModel>(),
			new ViewMap<WelcomePage, WelcomeModel>(),
			new DataViewMap<FiltersPage, FilterModel, SearchFilter>(),
			new ViewMap<HomePage, HomeModel>(),
			new DataViewMap<CreateUpdateCookbookPage, CreateUpdateCookbookModel, Cookbook>(),
			new ViewMap<LoginPage, LoginModel>(ResultData: typeof(Credentials)),
			new ViewMap<RegistrationPage, RegistrationModel>(),
			new ViewMap<NotificationsPage, NotificationModel>(),
			new ViewMap<ProfilePage, ProfileModel>(Data: new DataMap<SenservaUser>(), ResultData: typeof(ISenservaEntity)),
			new ViewMap<PoliciesPage, PoliciesModel>(Data: new DataMap<Policy>()),
			new ViewMap<TechniquePage, TechniqueModel>(Data: new DataMap<Technique>()),
			new ViewMap<ReportingPage, ReportingModel>(Data: new DataMap<Report>()),
			new ViewMap<ReportPage, ReportModel>(Data: new DataMap<Report>()),
			new ViewMap<CommandsPage, SenservaCommandModel>(Data: new DataMap<SenservaCommand>()),
			new ViewMap<FavoriteTechniquesPage, FavoriteTechniquesModel>(),
			new ViewMap<LiveDataPage, LiveDataModel>(),
			new DataViewMap<SearchPage, SearchModel, SearchFilter>(),
			new ViewMap<SettingsPage, SettingsModel>(Data: new DataMap<SenservaUser>()),
			new ViewMap<LiveCookingPage, RemediateModel>(Data: new DataMap<RemediateParameter>()),
			new ViewMap<CookbookDetailPage, CookbookDetailModel>(Data: new DataMap<Cookbook>()),
			new ViewMap<CompletedDialog>(),
			new ViewMap<GenericDialog, GenericDialogModel>(Data: new DataMap<DialogInfo>())
		);

		routes.Register(
			new RouteMap("", View: views.FindByViewModel<ShellModel>(),
				Nested: new RouteMap[]
				{
					new RouteMap("Welcome", View: views.FindByViewModel<WelcomeModel>()),
					new RouteMap("Login", View: views.FindByViewModel<LoginModel>()),
					new RouteMap("Register", View: views.FindByViewModel<RegistrationModel>()),
					new RouteMap("Main", View: views.FindByViewModel<MainModel>(), Nested:
					[
						#region Main Tabs
						new RouteMap("Home", View: views.FindByViewModel<HomeModel>(), IsDefault: true),
						new RouteMap("Search", View: views.FindByViewModel<SearchModel>()),
						new RouteMap("Reports", View: views.FindByViewModel<ReportingModel>()),
						new RouteMap("Commands", View: views.FindByViewModel<SenservaCommandModel>()),
						new RouteMap("Policies", View: views.FindByViewModel<PoliciesModel>()),
						new RouteMap("FavoriteTechniques", View: views.FindByViewModel<FavoriteTechniquesModel>()),
						#endregion

						new RouteMap("CookbookDetails", View: views.FindByViewModel<CookbookDetailModel>()),
						new RouteMap("UpdateCookbook", View: views.FindByViewModel<CreateUpdateCookbookModel>()),
						new RouteMap("CreateCookbook", View: views.FindByViewModel<CreateUpdateCookbookModel>()),

						new RouteMap("TechniqueDetails", View: views.FindByViewModel<TechniqueModel>()),
						new RouteMap("ReportPage", View: views.FindByViewModel<ReportModel>()),

						new RouteMap("Remediate", View: views.FindByViewModel<RemediateModel>()),
					]),
					new RouteMap("Notifications", View: views.FindByViewModel<NotificationModel>()),
					new RouteMap("Filter", View: views.FindByViewModel<FilterModel>()),
					new RouteMap("Reports", View: views.FindByViewModel<ReportingModel>()),
					new RouteMap("Commands", View: views.FindByViewModel<SenservaCommandModel>()),
					new RouteMap("Profile", View: views.FindByViewModel<ProfileModel>()),
					new RouteMap("Settings", View: views.FindByViewModel<SettingsModel>()),
					new RouteMap("Completed", View: views.FindByView<CompletedDialog>()),
					new RouteMap("Dialog", View: views.FindByView<GenericDialog>())
				}
			)
		);
	}
}
