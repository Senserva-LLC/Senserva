using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using Simeserva.Services.LiveData;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace Simeserva.Presentation;

public partial record HomeModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _techniqueService;
	private readonly IUserService _userService;
	private readonly IMessenger _messenger;
	private readonly ILiveDataService _liveDataService;

	public HomeModel(INavigator navigator, ITechniqueService techniques, IUserService userService, IMessenger messenger, ILiveDataService liveData)
	{
		_navigator = navigator;
		_techniqueService = techniques;
		_userService = userService;
		_messenger = messenger;
		_liveDataService = liveData;

	}

	public IEnumerable<ISeries> Series { get; set; } =
	   new[] { 2, 4, 1, 4, 3 }.AsPieSeries();

	public IListState<Technique> TrendingTechniques => ListState
		.Async(this, async ct => await _techniqueService.GetTrending(ct))
		.Observe(_messenger, r => r.Id);


	public IListFeed<CategoryWithCount> Categories => ListFeed.Async(async ct => await _techniqueService.GetCategoriesWithCount(ct));

	public IListFeed<Technique> RecentlyAdded => ListFeed.Async(async ct => await _techniqueService.GetRecent(ct));

	public IListFeed<LiveDataModel> LiveData => ListFeed.Async(async ct => await _liveDataService.GetAll(ct));

	public IFeed<SenservaUser> UserProfile => _userService.User;

	public async ValueTask ShowAll(CancellationToken ct) =>
		await _navigator.NavigateRouteAsync(this, route: "/Main/-/Search", data: new SearchFilter(FilterGroup: FilterGroup.Popular), cancellation: ct);

	public async ValueTask ShowAllRecentlyAdded(CancellationToken ct) =>
		await _navigator.NavigateRouteAsync(this, route: "/Main/-/Search", data: new SearchFilter(FilterGroup: FilterGroup.Recent), cancellation: ct);

	public async ValueTask CategorySearch(CategoryWithCount categoryWithCount, CancellationToken ct) =>
		await _navigator.NavigateRouteAsync(this, route: "/Main/-/Search", data: new SearchFilter(Category: categoryWithCount.Category), cancellation: ct);

	public async ValueTask FavoriteTechnique(Technique recipe, CancellationToken ct) =>
		await _techniqueService.Favorite(recipe, ct);
}
