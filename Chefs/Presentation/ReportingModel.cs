
namespace Simeserva.Presentation;

public partial record ReportingModel
{
	private readonly INavigator _navigator;
	private readonly IReportsService _service;
	private readonly IUserService _userService;
	private readonly IMessenger _messenger;

	public ReportingModel(
		Report report,
		INavigator navigator,
		IReportsService service,
		IUserService userService,
		IMessenger messenger)
	{
		_navigator = navigator;
		_service = service;
		_userService = userService;
		_messenger = messenger;

		Report = report ?? new Report();
	}

	public Report Report { get; }

	public IListState<Report> TrendingReports => ListState
	.Async(this, async ct => await _service.GetTrending(ct))
	.Observe(_messenger, r => r.Id);


	public IListFeed<CategoryWithCount> Categories => ListFeed.Async(async ct => await _service.GetCategoriesWithCount(ct));

	public IListFeed<Report> RecentlyAdded => ListFeed.Async(async ct => await _service.GetRecent(ct));

	public async ValueTask CategorySearch(CategoryWithCount categoryWithCount, CancellationToken ct) =>
	await _navigator.NavigateRouteAsync(this, route: "/Main/-/Search", data: new SearchFilter(Category: categoryWithCount.Category), cancellation: ct);


	// TODO put in name and type
	public string Title => $"Report {Report.Name} - {Report.Type} ";

	public IState<bool> IsFavorited => State.Value(this, () => Report.IsFavorite);

	public IState<SenservaUser> User => State.Async(this, async ct => await _userService.GetById(Report.UserId, ct))
		.Observe(_messenger, u => u.Id);

	public IFeed<SenservaUser> CurrentUser => Feed.Async(async ct => await _userService.GetCurrent(ct));

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
