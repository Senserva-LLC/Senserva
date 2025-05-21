
namespace Simeserva.Services.Techniques;

public class ReportsService(
	IUserService userService,
	IWritableOptions<SearchHistory> searchOptions,
	IMessenger messenger)
	: IReportsService
{
	private int _lastTextLength;

	public async Task<IImmutableList<Report>> GetReportsAsync()
	{
		var list = new List<Report>();

		list.Add(new Report());

		return list.ToImmutableList();
	}

	/// <summary>
	/// define the details structure as more is known
	/// </summary>
	/// <param name="recipeId"></param>
	/// <param name="ct"></param>
	/// <returns></returns>
	public async Task<IImmutableList<string>> GetDetails(Guid recipeId, CancellationToken ct)
	{
		var list = new List<string>();
		return list.ToImmutableList();
	}


	public async Task<IImmutableList<Compliance>> GetReviewsAsync() => new List<Compliance>() { new Compliance() { PublisherName = "Microsoft" } }.ToImmutableList();

	public async Task<IImmutableList<RemediationStep>> GetStepsAsync() => new List<RemediationStep>() { new RemediationStep() { Name = "step1" }, new RemediationStep() { Name = "step2" } }.ToImmutableList();

	public async Task<IImmutableList<Content>> GetContentsAsync() => new List<Content>().ToImmutableList();

	public async Task<IImmutableList<Category>> GetCategoriesAsync(CancellationToken ct) => new List<Category>() { new Category() }.ToImmutableList();

	public async Task<IImmutableList<Report>> GetAll(CancellationToken ct)
	{
		return await GetReportsAsync();
	}

	public async ValueTask<int> GetCount(Guid userId, CancellationToken ct)
	{
		//var countData = await api.Api.Technique.Count.GetAsync(q => q.QueryParameters.UserId = userId, cancellationToken: ct);
		return (await GetReportsAsync()).Count;
	}

	public async Task<IImmutableList<Report>> GetByCategory(int categoryId, CancellationToken ct)
	{
		var recipesData = await GetReportsAsync();
		return recipesData.Where(r => r.Category?.Id == categoryId).Select(r => r).ToImmutableList() ?? [];
	}

	public async Task<IImmutableList<CategoryWithCount>> GetCategoriesWithCount(CancellationToken ct)
	{
		var categories = await GetCategoriesAsync(ct);
		var tasks = categories.Select(async category =>
		{
			var recipesByCategory = await GetByCategory(category.Id ?? 0, ct);
			return new CategoryWithCount(recipesByCategory.Count, category);
		});

		var categoriesWithCount = await Task.WhenAll(tasks);
		return categoriesWithCount.ToImmutableList();
	}

	public async Task<IImmutableList<Report>> GetRecent(CancellationToken ct)
	{
		return await GetReportsAsync();
		//	return recipesData?.Select(r => new Technique(r)).OrderByDescending(x => x.Date).Take(7).ToImmutableList() ?? ImmutableList<Technique>.Empty;
	}

	public async Task<IImmutableList<Report>> GetTrending(CancellationToken ct)
	{
		return await GetReportsAsync();
	}

	public async Task<IImmutableList<Report>> GetPopular(CancellationToken ct)
	{
		return await GetReportsAsync();
	}

	public async Task<IImmutableList<Report>> Search(string term, SearchFilter filter, CancellationToken ct)
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
			return GetTechniquesByText(recipesToSearch, term);
		}
	}

	public IImmutableList<string> GetSearchHistory()
		=> searchOptions.Value.Searches.Take(3).ToImmutableList();

	///SecurityControl
	public async Task<IImmutableList<SecurityControl>> GetControls(Guid recipeId, CancellationToken ct)
	{
		return new List<SecurityControl>() { new SecurityControl("one", "two") }.ToImmutableList();
	}

	public async Task<IImmutableList<Content>> GetContent(Guid recipeId, CancellationToken ct) => new List<Content>() { new("one", "two") }.ToImmutableList();


	public async Task<IImmutableList<Compliance>> GetCompliance(Guid recipeId, CancellationToken ct)
	{
		return await GetReviewsAsync();
	}

	public async Task<IImmutableList<RemediationStep>> GetSteps(Guid recipeId, CancellationToken ct)
	{
		return await GetStepsAsync();
		//var stepsData = await api.Api.Technique[recipeId].Steps.GetAsync(cancellationToken: ct);
	}

	public async Task<IImmutableList<Report>> GetByUser(Guid userId, CancellationToken ct)
	{
		return await GetReportsAsync();
		//return recipesData?.Where(r => r.UserId == userId).Select(x => new Technique(x)).ToImmutableList() ?? ImmutableList<Technique>.Empty;
	}

	public async ValueTask<Compliance> CreateReview(Guid recipeId, string review, CancellationToken ct)
	{
		var reviewData = new Compliance { TechniqueId = recipeId, Description = review };
		return reviewData;
	}



	public IListState<Report> FavoritedTechniques => ListState<Report>.Async(this, GetFavorited);

	public async Task<IImmutableList<Report>> GetFavoritedWithPagination(uint pageSize, uint firstItemIndex, CancellationToken ct)
	{
		var favoritedTechniques = await GetFavorited(ct);
		return favoritedTechniques
			.Skip((int)firstItemIndex)
			.Take((int)pageSize)
			.ToImmutableList();
	}

	public async ValueTask Favorite(Report recipe, CancellationToken ct)
	{
		var currentUser = await userService.GetCurrent(ct);
		var updatedTechnique = recipe with { IsFavorite = !recipe.IsFavorite };

		if (updatedTechnique.IsFavorite)
		{
			await FavoritedTechniques.AddAsync(updatedTechnique, ct: ct);
		}
		else
		{
			await FavoritedTechniques.RemoveAllAsync(r => r.Id == updatedTechnique.Id, ct: ct);
		}

		messenger.Send(new EntityMessage<Report>(EntityChange.Updated, updatedTechnique));
	}

	public async ValueTask LikeReview(Compliance review, CancellationToken ct)
	{
		var updatedReview = new Compliance();
		messenger.Send(new EntityMessage<Compliance>(EntityChange.Updated, updatedReview));
	}

	public async ValueTask DislikeReview(Compliance review, CancellationToken ct)
	{
		var updatedReview = new Compliance();
		messenger.Send(new EntityMessage<Compliance>(EntityChange.Updated, updatedReview));
	}

	public async Task<IImmutableList<Report>> GetRecommended(CancellationToken ct)
	{
		return await GetReportsAsync();
	}

	public async Task<IImmutableList<Report>> GetFromChefs(CancellationToken ct)
	{
		return await GetReportsAsync();
	}

	private async ValueTask<IImmutableList<Report>> GetFavorited(CancellationToken ct)
	{
		return await GetReportsAsync();
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

	private IImmutableList<Report> GetTechniquesByText(IEnumerable<Report> recipes, string text)
		=> recipes
			.Where(r => r.Name?.Contains(text, StringComparison.OrdinalIgnoreCase) == true
						|| r.Category?.Name.ToString()?.Contains(text, StringComparison.OrdinalIgnoreCase) == true)
			.ToImmutableList();
}
