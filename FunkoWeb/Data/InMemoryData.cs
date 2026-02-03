namespace FunkoWeb.Data;

using FunkoWeb.Models;

public static class InMemoryData
{
    public static List<Category> Categories { get; set; } = new();
    public static List<Funko> Funkos { get; set; } = new();

    public static void Seed()
    {
        Categories.Clear();
        Funkos.Clear();

        var disney = new Category { Name = "Disney" };
        var marvel = new Category { Name = "Marvel" };
        var DragonBall = new Category { Name = "Anime" };
        var anime = new Category { Name = "Anime" };

        Categories.AddRange(new[] { disney, marvel, anime });

        Funkos.Add(new Funko 
        { 
            Name = "Stitch", 
            Price = 15.99, 
            Stock = 10, 
            Category = disney,
            Image = "https://m.media-amazon.com/images/I/51I7M8F6Z9L.jpg"
        });

        Funkos.Add(new Funko 
        { 
            Name = "Iron Man", 
            Price = 18.50, 
            Stock = 5, 
            Category = marvel,
            Image = "https://m.media-amazon.com/images/I/618WpY8A8vL.jpg"
        });
        
        Funkos.Add(new Funko 
        { 
            Name = "Goku Ultra Instinct", 
            Price = 22.00, 
            Stock = 3, 
            Category = anime,
            Image = "https://m.media-amazon.com/images/I/51S6m6K-4EL.jpg"
        });
        
        Funkos.Add(new Funko
        {
            Name = "Rock",
            Price = 15.99,
            Stock = 10,
            Category = anime,
            Image = "https://m.media-amazon.com/images/I/51S6m6K-5EL.jpg"
        });
    }
}