
namespace Chefs.Presentation;

public partial record RecipeDetailsModel
{
	private readonly INavigator _navigator;
	private readonly IRecipeService _recipeService;
	private readonly IUserService _userService;
	private readonly IMessenger _messenger;

	public RecipeDetailsModel(
		Recipe recipe,
		INavigator navigator,
		IRecipeService recipeService,
		IUserService userService,
		IMessenger messenger)
	{
		_navigator = navigator;
		_recipeService = recipeService;
		_userService = userService;
		_messenger = messenger;

		Recipe = recipe;
	}

	public Recipe Recipe { get; }

	public IImmutableList<Content> Controls => Recipe.Controls?.ToImmutableList() ?? [];

	public IState<bool> IsFavorited => State.Value(this, () => Recipe.IsFavorite);

	public IState<SenservaUser> User => State.Async(this, async ct => await _userService.GetById(Recipe.UserId, ct))
		.Observe(_messenger, u => u.Id);

	public IFeed<SenservaUser> CurrentUser => Feed.Async(async ct => await _userService.GetCurrent(ct));

	public IListFeed<RemediationStep> Steps => ListFeed.Async(async ct => await _recipeService.GetSteps(Recipe.Id, ct));

	public IListState<Compliance> Compliance => ListState
		.Async(this, async ct => await _recipeService.GetReviews(Recipe.Id, ct))
		.Observe(_messenger, r => r.Id);

	public async ValueTask Like(Compliance review, CancellationToken ct) =>
		await _recipeService.LikeReview(review, ct);

	public async ValueTask Dislike(Compliance review, CancellationToken ct) =>
		await _recipeService.DislikeReview(review, ct);

	public async ValueTask LiveCooking(IImmutableList<RemediationStep> steps) =>
		await _navigator.NavigateRouteAsync(this, "LiveCooking", data: new LiveCookingParameter(Recipe, steps));

	public async ValueTask Favorite(CancellationToken ct)
	{
		await _recipeService.Favorite(Recipe, ct);
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
