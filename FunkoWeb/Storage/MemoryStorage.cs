using FunkoWeb.Models;

namespace FunkoWeb.Storage;

public class MemoryStorage
{
    public List<Category> Categories { get; set; } = new()
    {
        new Category() { Id = Guid.NewGuid(), Name = "Disney" },
        new Category() { Id = Guid.NewGuid(), Name = "Marvels" }
    };
    
    public List<Funko> Funkos { get; set; }
    
    private static MemoryStorage _instance;
    public static MemoryStorage Instance => _instance ??= new MemoryStorage();
    
    private MemoryStorage() { }
}