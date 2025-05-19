namespace Chefs.Presentation;

public partial record LiveCookingParameter(Technique Technique, IImmutableList<RemediationStep> Steps);

public partial class LiveCookingModel
{
	private readonly ITechniqueService _recipeService;

	private readonly IImmutableList<RemediationStep> _steps;
	private readonly INavigator _navigator;

	public Technique Technique { get; }

	public IState<StepIterator> Steps => State.Value(this, () => new StepIterator(_steps));

	public IState<bool> Completed => State.Value(this, () => false);

	public LiveCookingModel(LiveCookingParameter parameter, ITechniqueService recipeService, INavigator navigator)
	{
		Technique = parameter.Technique;
		_recipeService = recipeService;
		_navigator = navigator;
		_steps = parameter.Steps;
	}

	public async ValueTask Complete()
	{
		await Completed.SetAsync(true);
	}

	public async ValueTask BackToLastStep()
	{
		await Completed.SetAsync(false);
	}

	public async ValueTask Favorite(CancellationToken ct)
	{
		await _recipeService.Favorite(Technique, ct);
		await _navigator.NavigateViewModelAsync<HomeModel>(this, qualifier: Qualifiers.ClearBackStack, cancellation: ct);
	}
}
