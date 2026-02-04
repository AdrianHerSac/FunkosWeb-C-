namespace FunkoWeb.Data;

using FunkoWeb.Models;
using FunkoWeb.Models.Domain;

public static class InMemoryData
{
    public static List<Category> Categories { get; set; } = new();
    public static List<Funko> Funkos { get; set; } = new();
    public static List<Rol> Roles { get; set; } = new();
    public static List<Usuario> Usuarios { get; set; } = new();

    public static void Seed()
    {
        Categories.Clear();
        Funkos.Clear();
        Roles.Clear();
        Usuarios.Clear();

        var adminRole = new Rol { Id = 1, Nombre = "Admin" };
        var userRole = new Rol { Id = 2, Nombre = "User" };
        Roles.AddRange(new[] { adminRole, userRole });

        Usuarios.Add(new Usuario
        {
            Id = 1,
            Email = "admin@funko.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
            Rol = adminRole
        });

        Usuarios.Add(new Usuario
        {
            Id = 2,
            Email = "user@funko.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("user123"),
            Rol = userRole
        });

        var disney = new Category { Name = "Disney" };
        var marvel = new Category { Name = "Marvel" };
        var serie = new Category { Name = "Serie" };
        var anime = new Category { Name = "Anime" };
        var dreamwork = new Category { Name = "Dreamwork" };

        Categories.AddRange(new[] { disney, marvel, anime, dreamwork });
        
        Funkos.Add(new Funko
        {
            Name = "Yoda",
            Price = 15.99,
            Stock = 5,
            Category = disney,
            Image = "/images/Funko-Yoda.jpg"
        });
        
        Funkos.Add(new Funko
        {
            Name = "Demogorgon",
            Price = 15.99,
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