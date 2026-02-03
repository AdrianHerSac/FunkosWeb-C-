using FunkoWeb.DTOs;
using FunkoWeb.Models;

namespace FunkoWeb.Mappers;

public static class FunkoMapper
{
    /// <summary>
    /// Convierte un FunkoCreateDto a una entidad Funko
    /// </summary>
    public static Funko ToEntity(this FunkoCreateDto dto, Category category)
    {
        return new Funko
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Price = dto.Price,
            Stock = dto.Stock,
            Image = dto.Image ?? "/images/default-funko.png",
            Category = category,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    /// <summary>
    /// Actualiza una entidad Funko existente con datos de FunkoUpdateDto
    /// </summary>
    public static Funko UpdateEntity(this Funko funko, FunkoUpdateDto dto, Category category)
    {
        funko.Name = dto.Name;
        funko.Price = dto.Price;
        funko.Stock = dto.Stock;
        funko.Category = category;
        funko.UpdatedAt = DateTime.Now;

        // Solo actualizar la imagen si se proporciona una nueva
        if (!string.IsNullOrWhiteSpace(dto.Image))
        {
            funko.Image = dto.Image;
        }

        return funko;
    }

    /// <summary>
    /// Convierte una entidad Funko a FunkoResponseDto
    /// </summary>
    public static FunkoResponseDto ToResponseDto(this Funko funko)
    {
        return new FunkoResponseDto
        {
            Id = funko.Id,
            Name = funko.Name,
            Price = funko.Price,
            Stock = funko.Stock,
            Image = funko.Image,
            Category = funko.Category.ToResponseDto(),
            CreatedAt = funko.CreatedAt,
            UpdatedAt = funko.UpdatedAt
        };
    }

    /// <summary>
    /// Convierte una lista de entidades Funko a lista de FunkoResponseDto
    /// </summary>
    public static List<FunkoResponseDto> ToResponseDtoList(this List<Funko> funkos)
    {
        return funkos.Select(f => f.ToResponseDto()).ToList();
    }
}
