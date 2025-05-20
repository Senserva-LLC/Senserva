using Chefs.Presentation.Extensions;

namespace Chefs.Presentation;

public partial record CreateUpdateCookbookModel
{
	const uint DefaultPageSize = 20;
	public IState<IImmutableList<Technique>> SelectedTechniques { get; }

	private readonly INavigator _navigator;
	private readonly ITechniqueService _recipeService;
	private readonly ICookbookService _cookbookService;
	private readonly IMessenger _messenger;
	private readonly Cookbook? _cookbook;

	public CreateUpdateCookbookModel(
		INavigator navigator,
		ITechniqueService recipeService,
		ICookbookService cookbookService,
		Cookbook? cookbook,
		IMessenger messenger)
	{
		_navigator = navigator;
		_recipeService = recipeService;
		_cookbookService = cookbookService;
		_messenger = messenger;

		if (cookbook is not null)
		{
			_cookbook = cookbook;
			Title = "Update Cookbook";
			SubTitle = "Manage cookbook's recipes";
			SaveButtonContent = "Apply change";
			IsCreate = false;
		}
		else
		{
			Title = "Create Cookbook";
			SubTitle = "Add recipes";
			SaveButtonContent = "Create cookbook";
			IsCreate = true;
		}
		SelectedTechniques = State.Value(this, () => _cookbook?.Techniques ?? ImmutableList<Technique>.Empty);

	}
	public bool IsCreate { get; }

	public string Title { get; }

	public string SubTitle { get; }

	public string SaveButtonContent { get; }

	public IState<Cookbook> Cookbook => State
		.Value(this, () => _cookbook ?? new Cookbook())
		.Observe(_messenger, cb => cb.Id);

	public IListFeed<Technique> Techniques => ListFeed
		.PaginatedAsync(
			async (PageRequest pageRequest, CancellationToken ct) =>
				await _recipeService.GetFavoritedWithPagination(pageRequest.DesiredSize ?? DefaultPageSize, pageRequest.CurrentCount, ct)
		)
		.Selection(SelectedTechniques);

	public async ValueTask Submit(CancellationToken ct)
	{
		var selectedTechniques = await SelectedTechniques;
		var cookbook = await Cookbook;

		if (selectedTechniques is { Count: > 0 } && cookbook is not null && cookbook.Name.HasValueTrimmed())
		{
			var response = IsCreate
				? await _cookbookService.Create(cookbook.Name!, selectedTechniques.ToImmutableList(), ct)
				: await _cookbookService.Update(cookbook, selectedTechniques, ct);

			if (IsCreate)
			{
				await _cookbookService.Save(response!, ct);
			}

			await _navigator.NavigateBackAsync(this);
		}
		else
		{
			await _navigator.ShowDialog(this, new DialogInfo("Error", "Please write a cookbook name and select one recipe."), ct);
		}
	}
}
