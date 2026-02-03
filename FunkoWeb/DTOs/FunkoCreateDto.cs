using System.ComponentModel.DataAnnotations;

namespace FunkoWeb.DTOs;

public class FunkoCreateDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, 10000, ErrorMessage = "El precio debe estar entre 0.01 y 10000")]
    public double Price { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser un valor positivo")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "Debe seleccionar una categoría")]
    public Guid CategoryId { get; set; }

    public string? Image { get; set; }
}
