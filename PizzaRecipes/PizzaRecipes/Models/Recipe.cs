namespace PizzaRecipes.Models;

public record Recipe
{
    public string Title { get; set; }
    public string Description { get; init; }
    public IEnumerable<string> Directions { get; init; }
    public IEnumerable<string> Ingredients { get;}
    public DateTime Updated { get; init; }
}
