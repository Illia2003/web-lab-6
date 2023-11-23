using System.ComponentModel.DataAnnotations;

namespace LR4.Entities;

public class Product : BaseEntity
{
    [Display(Name = "Назва продукту")]
    [Required(ErrorMessage = "Поле обов'язкове для введення")]
    [MinLength(5, ErrorMessage = "Мінімальна довжина - 5 символів")]
    [MaxLength(50, ErrorMessage = "Максимальна довжина - 50 символів")]
    [FirstLetterUpperCase]
    public string Title { get; set; }

    [Display(Name = "Опис продукту")]
    [Required(ErrorMessage = "Поле обов'язкове для введення")]
    [MinLength(15, ErrorMessage = "Мінімальна довжина - 15 символів")]
    [MaxLength(50, ErrorMessage = "Максимальна довжина - 50 символів")]
    [FirstLetterUpperCase]
    public string Description { get; set; }

    public ProductType ProductType { get; set; }

    [Required(ErrorMessage = "Додайте хоча-б один розмір")]
    public List<Size> Sizes { get; set; } = new();
}

public enum ProductType
{
    Dress = 0,
    Jeans = 1,
    Tshirt = 2,
    Coat = 3,
    Jacket = 4,
    DownJacket = 5
}