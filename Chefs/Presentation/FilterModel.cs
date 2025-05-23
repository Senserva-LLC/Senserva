namespace Simeserva.Presentation;

public partial record FilterModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _techniqueService;

	public FilterModel(SearchFilter filters, INavigator navigator, ITechniqueService recipeService)
	{
		_navigator = navigator;
		_techniqueService = recipeService;
		Filter = State.Value(this, () => filters);
	}
	public IState<SearchFilter> Filter { get; }
	public IEnumerable<FilterGroup> FilterGroups => Enum.GetValues(typeof(FilterGroup)).Cast<FilterGroup>();
	public IEnumerable<FixTime> Times => Enum.GetValues(typeof(FixTime)).Cast<FixTime>();
	public IEnumerable<Difficulty> Difficulties => Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();
	public IEnumerable<int> ItemsEffected => new int[] { 1, 2, 3, 4, 5 };
	public IListFeed<Category> Categories => ListFeed.Async(async ct => await _techniqueService.GetCategoriesAsync(ct));

	public async ValueTask ApplySearchFilter(SearchFilter filter) =>
		await _navigator.NavigateBackWithResultAsync(this, data: filter);

	public async ValueTask Reset(CancellationToken ct) =>
		await Filter.UpdateAsync(current => new SearchFilter(), ct);
}
