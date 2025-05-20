namespace Chefs.Services.Techniques;

/// <summary>
/// Implements recipe related methods
/// </summary>
public interface ITechniqueService
{
	/// <summary>
	/// Techniques method
	/// </summary>
	/// <param name="ct"></param>
	/// <returns>
	/// GetTechniquesAsync each recipe from api
	/// </returns>
	public Task<IImmutableList<Technique>> GetAll(CancellationToken ct);

	/// <summary>
	/// Techniques method
	/// </summary>
	/// <param name="ct"></param>
	/// <returns>
	/// GetTechniquesAsync recipes count by user
	/// </returns>
	ValueTask<int> GetCount(Guid userId, CancellationToken ct);

	/// <summary>
	/// Favorited recipes.
	/// </summary>
	IListState<Technique> FavoritedTechniques { get; }

	/// <summary>
	/// Techniques with a specific category
	/// </summary>
	/// <param name="categoryId">The specific category to filter recipes</param>
	/// <param name="ct"></param>
	/// <returns>
	/// GetTechniquesAsync each recipe from api filter by a category
	/// </returns>
	public Task<IImmutableList<Technique>> GetByCategory(int categoryId, CancellationToken ct);

	/// <summary>
	/// Categories from api
	/// </summary>
	/// <param name="ct"></param>
	/// <returns>
	/// GetTechniquesAsync each category from api
	/// </returns>
	public Task<IImmutableList<Category>> GetCategoriesAsync(CancellationToken ct);

	/// <summary>
	/// Categories from api with count
	/// </summary>
	/// <param name="ct"></param>
	/// <returns>
	/// GetTechniquesAsync each category from api with their corresponding count
	/// </returns>
	public Task<IImmutableList<CategoryWithCount>> GetCategoriesWithCount(CancellationToken ct);

	/// <summary>
	/// Techniques in trending
	/// </summary>
	/// <param name="ct"></param>
	/// <returns>
	/// GetTechniquesAsync recipes filter in trending now
	/// </returns>
	public Task<IImmutableList<Technique>> GetTrending(CancellationToken ct);

	/// <summary>
	/// Popular Techniques
	/// </summary>
	/// <param name="ct"></param>
	/// <returns>
	/// GetTechniquesAsync popular recipes filter
	/// </returns>
	public Task<IImmutableList<Technique>> GetPopular(CancellationToken ct);

	/// <summary>
	/// Techniques recently added
	/// </summary>
	/// <param name="ct"></param>
	/// <returns>
	/// GetTechniquesAsync recent recipes or new recipes
	/// </returns>
	public Task<IImmutableList<Technique>> GetRecent(CancellationToken ct);

	/// <summary>
	/// Filter recipes from api
	/// </summary>
	/// <param name="term">The search term</param>
	/// <param name="ct"></param>
	/// <returns>
	/// GetTechniquesAsync recipes filter by different options selected by the user
	/// </returns>
	public Task<IImmutableList<Technique>> Search(string term, SearchFilter filter, CancellationToken ct);

	/// <summary>
	/// GetTechniquesAsync recipe's reviews
	/// </summary>
	/// <param name="recipeId">id from the recipe</param>
	/// <param name="ct"></param>
	/// <returns>
	/// Technique's reviews
	/// </returns>
	public Task<IImmutableList<Compliance>> GetCompliance(Guid recipeId, CancellationToken ct);

	/// <summary>
	/// Save recipe
	/// </summary>
	/// <param name="recipe"> recipe to save </param>
	/// <param name="ct"></param>
	/// <returns>
	/// </returns>
	ValueTask Favorite(Technique recipe, CancellationToken ct);

	/// <summary>
	/// Create review for a recipe
	/// </summary>
	/// <param name="review"> review to create </param>
	/// <param name="ct"></param>
	/// <returns>
	/// </returns>
	ValueTask<Compliance> CreateReview(Guid recipeId, string review, CancellationToken ct);

	/// <summary>
	/// GetTechniquesAsync review's steps
	/// </summary>
	/// <param name="recipeId">id from the recipe</param>
	/// <param name="ct"></param>
	/// <returns>
	/// Technique's steps
	/// </returns>
	public Task<IImmutableList<RemediationStep>> GetSteps(Guid recipeId, CancellationToken ct);

	public Task<IImmutableList<SecurityControl>> GetControls(Guid recipeId, CancellationToken ct);

	public Task<IImmutableList<Content>> GetContent(Guid recipeId, CancellationToken ct);

	/// <summary>
	/// define the details structure as more is known
	/// </summary>
	/// <param name="recipeId"></param>
	/// <param name="ct"></param>
	/// <returns></returns>
	public Task<IImmutableList<string>> GetDetails(Guid recipeId, CancellationToken ct);

	/// <summary>
	/// Techniques by user
	/// </summary>
	/// <param name="ct"></param>
	/// <returns>
	/// SenservaUser's recipes
	/// </returns>
	public Task<IImmutableList<Technique>> GetByUser(Guid userId, CancellationToken ct);

	/// <summary>
	/// Techniques favorited by the current user, supports pagination
	/// </summary>
	/// <param name="pageSize">number of items to display per page</param>
	/// <param name="firstItemIndex">index of the first item on the requested page</param>
	/// <param name="ct"></param>
	/// <returns>
	/// Current user's recipes within the requested page
	/// </returns>
	public Task<IImmutableList<Technique>> GetFavoritedWithPagination(uint pageSize, uint firstItemIndex, CancellationToken ct);

	public Task<IImmutableList<Technique>> GetRecommended(CancellationToken ct);

	public Task<IImmutableList<Technique>> GetFromChefs(CancellationToken ct);

	IImmutableList<string> GetSearchHistory();
}
