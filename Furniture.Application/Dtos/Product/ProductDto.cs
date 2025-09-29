using Furniture.Domain.Entities;

namespace Furniture.Application.Dtos.Product;
/// <summary>DTO товара</summary>
/// <remarks>
/// <para>Класс представляет DTO для <see cref="Furniture.Domain.Entities.Product"/>> </para>
/// </remarks>>
public class ProductDto
{
    /// <summary>Id товара.</summary>>
    public int Id { get; set; }
    /// <summary>Наименование товара.</summary>>
    public string Name { get; set; } = string.Empty;
    /// <summary>Описание товара.</summary>>
    public string Description { get; set; } = string.Empty;
    /// <summary>Категория товара.</summary>>
    public string Category { get; set; } = string.Empty;
    /// <summary>Цвет товара.</summary>>
    public string Color { get; set; } = string.Empty;
    /// <summary>Материал товара.</summary>>
    public string Material { get; set; } = string.Empty;
    /// <summary>Цена товара.</summary>>
    public decimal Price { get; set; }
    /// <summary>Ширина товара.</summary>>
    public double Width { get; set; }
    /// <summary>Высота товара.</summary>>
    public double Height { get; set; }
    /// <summary>Глубина товара.</summary>>
    public double Depth { get; set; }
    /// <summary>Имя продавца.</summary>>
    public string WorkerName { get; set; } = string.Empty;
    /// <summary>Дата добавления товара.</summary>>
    public DateTime CreatedOn { get; set; }
    /// <summary>Дата изменения товара.</summary>>
    public DateTime ModifiedOn { get; set; }
    /// <summary>Изображения товара.</summary>>
    public List<string> ImageUrls { get; set; } = new();
    /// <summary>Основное изображение товара.</summary>>
    public string? MainImageUrl { get; set; }
    
}