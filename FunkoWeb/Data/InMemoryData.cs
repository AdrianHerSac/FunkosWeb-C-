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
        var dreamwork = new Category { Name = "Dreamwork" };
        var marvel = new Category { Name = "Marvel" };
        var anime = new Category { Name = "Anime" };
        var serie = new Category { Name = "Serie" };

        Categories.AddRange(new[] { disney, dreamwork, marvel, anime,  serie });

        Funkos.Add(new Funko 
        { 
            Name = "Yoda", 
            Price = 35.99, 
            Stock = 10, 
            Category = disney,
            Image = "/images/Funko-Yoda.jpg"
        });

        Funkos.Add(new Funko
        {
            Name = "Demogorgon",
            Price = 35.99,
            Stock = 10,
            Category = serie,
            Image = "/images/Funko-Demogorgon.jpg"
        });

        Funkos.Add(new Funko 
        { 
            Name = "Stitch", 
            Price = 15.99, 
            Stock = 10, 
            Category = disney,
            Image = "/images/Funko-Stitch.jpg"
        });

        Funkos.Add(new Funko 
        { 
            Name = "Iron Man", 
            Price = 18.50, 
            Stock = 5, 
            Category = marvel,
            Image = "/images/Funko-IronMan.jpg"
        });
        
        Funkos.Add(new Funko 
        { 
            Name = "Goku Ultra Instinct", 
            Price = 41.99, 
            Stock = 3, 
            Category = anime,
            Image = "/images/Funko GokuUltraInstinct.jpg"
        });
        
        Funkos.Add(new Funko
        {
            Name = "Desdentao",
            Price = 15.99,
            Stock = 10,
            Category = dreamwork,
            Image = "/images/Funko-Desdentao.jpg"
        });
    }
}