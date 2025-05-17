namespace Chefs.Presentation;

public partial record ShellModel
{
	private readonly INavigator _navigator;

	public ShellModel(INavigator navigator)
	{
		_navigator = navigator;

		_ = Start();
	}

#if DEBUG
	public async Task Start() => await _navigator.NavigateViewModelAsync<MainModel>(this);
#else
public async Task Start() => await _navigator.NavigateViewModelAsync<WelcomeModel>(this);
#endif


}
