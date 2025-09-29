using Microsoft.AspNetCore.Http;

namespace Furniture.Application.Dtos.Product;
/// <summary>DTO товара для создания.</summary>
/// <remarks>
/// <para>Класс представляет DTO для создания класса <see cref="Furniture.Domain.Entities.Product"/>> </para>
/// </remarks>>
public class CreateProductWithImageDto
{
    ///<summary>Наименование товара.</summary>
    public string Name { get; set; } = string.Empty;
    ///<summary>Описание товара.</summary>
    public string Description { get; set; } = string.Empty;
    ///<summary>Категория товара.</summary>
    public string Category { get; set; } = string.Empty;
    ///<summary>Цвет товара.</summary>
    public string Color { get; set; } = string.Empty;
    ///<summary>Материал товара.</summary>
    public string Material { get; set; } = string.Empty;
    ///<summary>Цена товара.</summary>
    public decimal Price { get; set; }
    ///<summary>Ширина товара.</summary>
    public int Width { get; set; }
    ///<summary>Высота товара.</summary>
    public int Height { get; set; }
    ///<summary>Глубина товара.</summary>
    public int Depth { get; set; }
    ///<summary>Изображения товара.</summary>
    public List<IFormFile> Images { get; set; } = new();
    
}