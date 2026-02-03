using FunkoWeb.DTOs;
using FunkoWeb.Models;

namespace FunkoWeb.Mappers;

public static class CategoryMapper
{
    /// <summary>
    /// Convierte una entidad Category a CategoryResponseDto
    /// </summary>
    public static CategoryResponseDto ToResponseDto(this Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    /// <summary>
    /// Convierte una lista de entidades Category a lista de CategoryResponseDto
    /// </summary>
    public static List<CategoryResponseDto> ToResponseDtoList(this List<Category> categories)
    {
        return categories.Select(c => c.ToResponseDto()).ToList();
    }
}
