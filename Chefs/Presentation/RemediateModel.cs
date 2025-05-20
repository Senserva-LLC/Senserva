namespace Simeserva.Presentation;

public partial record RemediateParameter(Technique Technique, IImmutableList<RemediationStep> Steps);

public partial class RemediateModel
{
	private readonly ITechniqueService _techniqueService;

	private readonly IImmutableList<RemediationStep> _steps;
	private readonly INavigator _navigator;

	public Technique Technique { get; }

	public IState<StepIterator> Steps => State.Value(this, () => new StepIterator(_steps));

	public IState<bool> Completed => State.Value(this, () => false);

	public RemediateModel(RemediateParameter parameter, ITechniqueService recipeService, INavigator navigator)
	{
		Technique = parameter.Technique;
		_techniqueService = recipeService;
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
		await _techniqueService.Favorite(Technique, ct);
		await _navigator.NavigateViewModelAsync<HomeModel>(this, qualifier: Qualifiers.ClearBackStack, cancellation: ct);
	}
}
