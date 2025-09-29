using Microsoft.AspNetCore.Http;

namespace Furniture.Application.Dtos.Product;

/// <summary>DTO товара для изменения.</summary>
/// <remarks>
/// <para>Класс представляет DTO для изменения существующего <see cref="Furniture.Domain.Entities.Product"/>> </para>
/// </remarks>>
public class UpdateProductWithImageDto
{
    ///<summary>Наименование товара.</summary>
    public string Name { get; set; } = string.Empty;
    ///<summary>Описание товара.</summary>
    public string Description { get; set; } = string.Empty;
    ///<summary>Цвет товара.</summary>
    public string Color { get; set; } = string.Empty;
    ///<summary>Материал товара.</summary>
    public string Material { get; set; } = string.Empty;
    ///<summary>Цена товара.</summary>
    public decimal Price { get; set; }
    ///<summary>Ширина товара.</summary>
    public double Width { get; set; }
    ///<summary>Высота товара.</summary>
    public double Height { get; set; }
    ///<summary>Глубина товара.</summary>
    public double Depth { get; set; }
    ///<summary>Новые изображения товара.</summary>
    public List<IFormFile> NewImages { get; set; } = new();
}