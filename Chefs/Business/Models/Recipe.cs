namespace Chefs.Business.Models;

public partial record Recipe : IChefEntity
{
	public Guid Id { get; init; }
	public Guid UserId { get; init; }
	public string? ImageUrl { get; init; }
	public string? Name { get; init; }
	public int Serves { get; init; }
	public TimeSpan CookTime { get; init; }
	public Difficulty Difficulty { get; init; }
	public string? Calories { get; init; }
	public string? Details { get; init; }
	public Category Category { get; init; }
	public DateTimeOffset Date { get; init; }
	public bool IsFavorite { get; init; }
	public Nutrition Nutrition { get; init; }

	public Recipe() 
	{ 
		Id = Guid.NewGuid();
		UserId = Guid.NewGuid();
		Name = "test";
		Date = DateTimeOffset.UtcNow;
		Nutrition = new Nutrition();
		Category = new Category();
		Difficulty = Difficulty.Beginner;
		CookTime = TimeSpan.FromMinutes(30);
	}


	//remove "kcal" unit from Calories property
	public string? CaloriesAmount => Calories?.Length > 4 ? Calories.Remove(Calories.Length - 4) : Calories;
	public string TimeCal
	{
		get
		{
			return CookTime > TimeSpan.FromHours(1)
				? $"{CookTime:%h} hour {CookTime:%m} mins • {Calories}"
				: $"{CookTime:%m} mins • {Calories}";
		}
	}

}
