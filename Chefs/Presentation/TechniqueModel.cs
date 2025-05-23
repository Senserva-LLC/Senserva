
using Siemserva.Business.Models;
using Siemserva.Services.Target;

namespace Simeserva.Presentation;

public partial record TechniqueModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _techniqueService;
	private readonly IUserService _userService;
	private readonly IMessenger _messenger;
	private readonly ITargetService _targetService;

	public TechniqueModel(
		Technique technique,
		INavigator navigator,
		ITechniqueService recipeService,
		IUserService userService,
		IMessenger messenger, ITargetService target)
	{
		_navigator = navigator;
		_techniqueService = recipeService;
		_userService = userService;
		_messenger = messenger;
		_targetService = target;

		Technique = technique;
	}

	public Technique Technique { get; }

	public IListFeed<string> Overviews => ListFeed.Async(async ct => await _targetService.GetOverview(ct));

	// TODO put in name and type
	public string Title => $"Technique {Technique.Name} - {Technique.Type} ";

	public IState<bool> IsFavorited => State.Value(this, () => Technique.IsFavorite);

	public IState<SenservaUser> User => State.Async(this, async ct => await _userService.GetById(Technique.UserId, ct))
		.Observe(_messenger, u => u.Id);

	public IFeed<SenservaUser> CurrentUser => Feed.Async(async ct => await _userService.GetCurrent(ct));

	public IListFeed<RemediationStep> Steps => ListFeed.Async(async ct => await _techniqueService.GetSteps(Technique.Id, ct));

	public IListFeed<SecurityControl> Controls => ListFeed.Async(async ct => await _techniqueService.GetControls(Technique.Id, ct));

	public IListFeed<Content> TechniqueContent => ListFeed.Async(async ct => await _techniqueService.GetContent(Technique.Id, ct));

	public IListFeed<string> KeyDetails => ListFeed.Async(async ct => await _techniqueService.GetDetails(Technique.Id, ct));

	public IListState<Compliance> Compliance => ListState
		.Async(this, async ct => await _techniqueService.GetCompliance(Technique.Id, ct))
		.Observe(_messenger, r => r.Id);

	public async ValueTask Remediate(IImmutableList<RemediationStep> steps) =>
		await _navigator.NavigateRouteAsync(this, "Remediate", data: new RemediateParameter(Technique, steps));

	public async ValueTask Favorite(CancellationToken ct)
	{
		await _techniqueService.Favorite(Technique, ct);
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
