namespace Chefs.Business.Models;

public record Ingredient
{
	public string? UrlIcon { get; set; }
	public string? Name { get; init; }
	public string? Quantity { get; init; }
	public string? NameQuantity => $"{Name} - {Quantity}";
}
