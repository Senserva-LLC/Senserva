
namespace Simeserva.Presentation;

public partial record CommandModel
{
	private readonly INavigator _navigator;
	private readonly ICommandsService _service;
	private readonly IUserService _userService;
	private readonly IMessenger _messenger;

	public CommandModel(
		SenservaCommand command,
		INavigator navigator,
		ICommandsService recipeService,
		IUserService userService,
		IMessenger messenger)
	{
		_navigator = navigator;
		_service = recipeService;
		_userService = userService;
		_messenger = messenger;

		Command = command;
	}

	public SenservaCommand Command { get; }

	// TODO put in name and type
	public string Title => $"Command {Command.Name} - {Command.Type} ";

	public IState<bool> IsFavorited => State.Value(this, () => Command.IsFavorite);

	public IState<SenservaUser> User => State.Async(this, async ct => await _userService.GetById(Command.UserId, ct))
		.Observe(_messenger, u => u.Id);

	public IFeed<SenservaUser> CurrentUser => Feed.Async(async ct => await _userService.GetCurrent(ct));

	/// <summary>
	/// TODO easist way to share?
	/// </summary>
	/// <param name="ct"></param>
	/// <returns></returns>
	public async Task Share(CancellationToken ct)
	{

	}
}
