
namespace Chefs.Presentation;

public partial record TechniqueDetailsModel
{
	private readonly INavigator _navigator;
	private readonly ITechniqueService _recipeService;
	private readonly IUserService _userService;
	private readonly IMessenger _messenger;

	public TechniqueDetailsModel(
		Technique recipe,
		INavigator navigator,
		ITechniqueService recipeService,
		IUserService userService,
		IMessenger messenger)
	{
		_navigator = navigator;
		_recipeService = recipeService;
		_userService = userService;
		_messenger = messenger;

		Technique = recipe;
	}

	public Technique Technique { get; }

	public IImmutableList<Content> Controls => Technique.Controls?.ToImmutableList() ?? [];

	public IState<bool> IsFavorited => State.Value(this, () => Technique.IsFavorite);

	public IState<SenservaUser> User => State.Async(this, async ct => await _userService.GetById(Technique.UserId, ct))
		.Observe(_messenger, u => u.Id);

	public IFeed<SenservaUser> CurrentUser => Feed.Async(async ct => await _userService.GetCurrent(ct));

	public IListFeed<RemediationStep> Steps => ListFeed.Async(async ct => await _recipeService.GetSteps(Technique.Id, ct));

	public IListFeed<string> KeyDetails => ListFeed.Async(async ct => await _recipeService.GetDetails(Technique.Id, ct));

	public IListState<Compliance> Compliance => ListState
		.Async(this, async ct => await _recipeService.GetReviews(Technique.Id, ct))
		.Observe(_messenger, r => r.Id);

	public async ValueTask Like(Compliance review, CancellationToken ct) =>
		await _recipeService.LikeReview(review, ct);

	public async ValueTask Dislike(Compliance review, CancellationToken ct) =>
		await _recipeService.DislikeReview(review, ct);

	public async ValueTask LiveCooking(IImmutableList<RemediationStep> steps) =>
		await _navigator.NavigateRouteAsync(this, "LiveCooking", data: new LiveCookingParameter(Technique, steps));

	public async ValueTask Favorite(CancellationToken ct)
	{
		await _recipeService.Favorite(Technique, ct);
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
