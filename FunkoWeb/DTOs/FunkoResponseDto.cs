namespace FunkoWeb.DTOs;

public class FunkoResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Stock { get; set; }
    public string Image { get; set; } = string.Empty;
    public CategoryResponseDto Category { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
