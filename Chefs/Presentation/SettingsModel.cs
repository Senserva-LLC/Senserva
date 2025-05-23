using Simeserva.Presentation.Messages;
using Simeserva.Services.Settings;
using AppTheme = Uno.Extensions.Toolkit.AppTheme;

namespace Simeserva.Presentation;

public partial record SettingsModel
{
	private readonly IUserService _userService;
	private readonly IThemeService _themeService;
	private readonly ISettingsService _settingsService;
	private readonly SenservaUser _user;

	public SettingsModel(
		IThemeService themeService,
		IUserService userService,
		ISettingsService settingsService,
		SenservaUser user)
	{
		_userService = userService;
		_themeService = themeService;
		_settingsService = settingsService;
		_user = user;
	}

	public IState<AppConfig> Settings => State
		.Async(this, _settingsService.GetSettings)
		.ForEach(async (settings, ct) =>
		{
			if (settings is { })
			{
				var isDark = (settings.IsDark ?? false);
				await _themeService.SetThemeAsync(isDark ? Uno.Extensions.Toolkit.AppTheme.Dark : Uno.Extensions.Toolkit.AppTheme.Light);
				await _settingsService.SetSettings(settings, ct);

				WeakReferenceMessenger.Default.Send(new ThemeChangedMessage(isDark));
			}
		});
	public IState<SenservaUser> Profile => State
		.Value(this, () => _user)
		.ForEach(async (profile, ct) =>
		{
			if (profile is null)
			{
				return;
			}

			await _userService.Update(profile, ct);
		});
}
