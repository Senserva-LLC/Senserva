
namespace Chefs.Services.Recipes;

public class RecipeService(
	IUserService userService,
	IWritableOptions<SearchHistory> searchOptions,
	IMessenger messenger)
	: IRecipeService
{
	private int _lastTextLength;

	public async Task<IImmutableList<Recipe>> GetRecipesAsync()
	{
		var list = new List<Recipe>();

		list.Add(new Recipe());

		return list.ToImmutableList();
	}

	public async Task<IImmutableList<Review>> GetReviewsAsync() => new List<Review>().ToImmutableList();

	public async Task<IImmutableList<Step>> GetStepsAsync() => new List<Step>().ToImmutableList();

	public async Task<IImmutableList<Ingredient>> GetIngredientsAsync() => new List<Ingredient>().ToImmutableList();

	public async Task<IImmutableList<Category>> GetCategoriesAsync() => new List<Category>().ToImmutableList();

	public async Task<IImmutableList<Recipe>> GetAll(CancellationToken ct)
	{
		return await GetRecipesAsync();
	}

	public async ValueTask<int> GetCount(Guid userId, CancellationToken ct)
	{
		//var countData = await api.Api.Recipe.Count.GetAsync(q => q.QueryParameters.UserId = userId, cancellationToken: ct);
		return (await GetRecipesAsync()).Count;
	}

	public async Task<IImmutableList<Recipe>> GetByCategory(int categoryId, CancellationToken ct)
	{
		//var recipesData = await api.Api.Recipe.GetAsync(cancellationToken: ct);
		//return recipesData?.Where(r => r.Category?.Id == categoryId).Select(r => new Recipe(r)).ToImmutableList() ?? ImmutableList<Recipe>.Empty;
		return await GetRecipesAsync();
	}

	public async Task<IImmutableList<Category>> GetCategories(CancellationToken ct)
	{
		return await GetCategoriesAsync();
	}

	public async Task<IImmutableList<CategoryWithCount>> GetCategoriesWithCount(CancellationToken ct)
	{
		var categories = await GetCategories(ct);
		var tasks = categories.Select(async category =>
		{
			var recipesByCategory = await GetByCategory(category.Id ?? 0, ct);
			return new CategoryWithCount(recipesByCategory.Count, category);
		});

		var categoriesWithCount = await Task.WhenAll(tasks);
		return categoriesWithCount.ToImmutableList();
	}

	public async Task<IImmutableList<Recipe>> GetRecent(CancellationToken ct)
	{
		return await GetRecipesAsync();
	//	return recipesData?.Select(r => new Recipe(r)).OrderByDescending(x => x.Date).Take(7).ToImmutableList() ?? ImmutableList<Recipe>.Empty;
	}

	public async Task<IImmutableList<Recipe>> GetTrending(CancellationToken ct)
	{
		return await GetRecipesAsync();
	}

	public async Task<IImmutableList<Recipe>> GetPopular(CancellationToken ct)
	{
		return await GetRecipesAsync();
	}

	public async Task<IImmutableList<Recipe>> Search(string term, SearchFilter filter, CancellationToken ct)
	{
		var recipesToSearch = filter.FilterGroup switch
		{
			FilterGroup.Popular => await GetPopular(ct),
			FilterGroup.Trending => await GetTrending(ct),
			FilterGroup.Recent => await GetRecent(ct),
			_ => await GetAll(ct)
		};

		if (string.IsNullOrWhiteSpace(term))
		{
			_lastTextLength = 0;
			return recipesToSearch;
		}
		else
		{
			await SaveSearchHistory(term);
			return GetRecipesByText(recipesToSearch, term);
		}
	}

	public IImmutableList<string> GetSearchHistory()
		=> searchOptions.Value.Searches.Take(3).ToImmutableList();

	public async Task<IImmutableList<Review>> GetReviews(Guid recipeId, CancellationToken ct)
	{
		return await GetReviewsAsync();
	}

	public async Task<IImmutableList<Step>> GetSteps(Guid recipeId, CancellationToken ct)
	{
		return await GetStepsAsync();
		//var stepsData = await api.Api.Recipe[recipeId].Steps.GetAsync(cancellationToken: ct);
	}

	public async Task<IImmutableList<Ingredient>> GetIngredients(Guid recipeId, CancellationToken ct)
	{
		//var ingredientsData = await api.Api.Recipe[recipeId].Ingredients.GetAsync(cancellationToken:
		//ct);
		return await GetIngredientsAsync();
	}

	public async Task<IImmutableList<Recipe>> GetByUser(Guid userId, CancellationToken ct)
	{
		return await GetRecipesAsync();
		//return recipesData?.Where(r => r.UserId == userId).Select(x => new Recipe(x)).ToImmutableList() ?? ImmutableList<Recipe>.Empty;
	}

	public async ValueTask<Review> CreateReview(Guid recipeId, string review, CancellationToken ct)
	{
		var reviewData = new Review { RecipeId = recipeId, Description = review };
		return reviewData;
	}

	public IListState<Recipe> FavoritedRecipes => ListState<Recipe>.Async(this, GetFavorited);

	public async Task<IImmutableList<Recipe>> GetFavoritedWithPagination(uint pageSize, uint firstItemIndex, CancellationToken ct)
	{
		var favoritedRecipes = await GetFavorited(ct);
		return favoritedRecipes
			.Skip((int)firstItemIndex)
			.Take((int)pageSize)
			.ToImmutableList();
	}

	public async ValueTask Favorite(Recipe recipe, CancellationToken ct)
	{
		var currentUser = await userService.GetCurrent(ct);
		var updatedRecipe = recipe with { IsFavorite = !recipe.IsFavorite };

		if (updatedRecipe.IsFavorite)
		{
			await FavoritedRecipes.AddAsync(updatedRecipe, ct: ct);
		}
		else
		{
			await FavoritedRecipes.RemoveAllAsync(r => r.Id == updatedRecipe.Id, ct: ct);
		}

		messenger.Send(new EntityMessage<Recipe>(EntityChange.Updated, updatedRecipe));
	}

	public async ValueTask LikeReview(Review review, CancellationToken ct)
	{
		var updatedReview = new Review();
		messenger.Send(new EntityMessage<Review>(EntityChange.Updated, updatedReview));
	}

	public async ValueTask DislikeReview(Review review, CancellationToken ct)
	{
		var updatedReview = new Review();
		messenger.Send(new EntityMessage<Review>(EntityChange.Updated, updatedReview));
	}

	public async Task<IImmutableList<Recipe>> GetRecommended(CancellationToken ct)
	{
		return await GetRecipesAsync();
	}

	public async Task<IImmutableList<Recipe>> GetFromChefs(CancellationToken ct)
	{
		return await GetRecipesAsync();
	}

	private async ValueTask<IImmutableList<Recipe>> GetFavorited(CancellationToken ct)
	{
		return await GetRecipesAsync();
	}

	private async Task SaveSearchHistory(string text)
	{
		if (_lastTextLength <= text.Length) _lastTextLength = text.Length;

		var searchHistory = searchOptions.Value.Searches;
		if (!string.IsNullOrWhiteSpace(text))
		{
			if (searchHistory.Count == 0 || _lastTextLength == 1)
			{
				await searchOptions.UpdateAsync(h => h with { Searches = searchHistory.Prepend(text).ToList() });
			}
			else if (searchHistory.FirstOrDefault() is { } latestTerm
					 && (text.Contains(latestTerm) || latestTerm.Contains(text))
					 && _lastTextLength == text.Length)
			{
				await searchOptions.UpdateAsync(h => h with
				{
					Searches = searchHistory.Skip(1).Prepend(text).ToList(),
				});
			}
		}
	}

	private IImmutableList<Recipe> GetRecipesByText(IEnumerable<Recipe> recipes, string text)
		=> recipes
			.Where(r => r.Name?.Contains(text, StringComparison.OrdinalIgnoreCase) == true
						|| r.Category?.Name?.Contains(text, StringComparison.OrdinalIgnoreCase) == true)
			.ToImmutableList();
}
