
namespace Simeserva.Presentation;

public partial record ReportModel
{
	private readonly INavigator _navigator;
	private readonly IReportsService _service;
	private readonly IUserService _userService;
	private readonly IMessenger _messenger;

	public ReportModel(
		Report report,
		INavigator navigator,
		IReportsService recipeService,
		IUserService userService,
		IMessenger messenger)
	{
		_navigator = navigator;
		_service = recipeService;
		_userService = userService;
		_messenger = messenger;

		Report = report;
	}

	public Report Report { get; }

	// TODO put in name and type
	public string Title => $"Report {Report.Name} - {Report.Type} ";

	public IState<bool> IsFavorited => State.Value(this, () => Report.IsFavorite);

	public IState<SenservaUser> User => State.Async(this, async ct => await _userService.GetById(Report.UserId, ct))
		.Observe(_messenger, u => u.Id);

	public IFeed<SenservaUser> CurrentUser => Feed.Async(async ct => await _userService.GetCurrent(ct));

	public IListFeed<Content> TechniqueContent => ListFeed.Async(async ct => await _service.GetContent(Report.Id, ct));

	public IListFeed<string> KeyDetails => ListFeed.Async(async ct => await _service.GetDetails(Report.Id, ct));

	public IListState<Compliance> Compliance => ListState
		.Async(this, async ct => await _service.GetCompliance(Report.Id, ct))
		.Observe(_messenger, r => r.Id);


	public async ValueTask Favorite(CancellationToken ct)
	{
		await _service.Favorite(Report, ct);
		await IsFavorited.UpdateAsync(s => !s);
	}

	/// <summary>
	/// TODO easist way to share?
	/// </summary>
	/// <param name="ct"></param>
	/// <returns></returns>
	public async Task Share(CancellationToken ct)
	{

	}
}
