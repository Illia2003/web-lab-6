using System.ComponentModel.DataAnnotations;

namespace LR4.Entities;

public class Size : BaseEntity
{
    [Required(ErrorMessage = "Поле обов'язкове для введення")]
    [MaxLength(5, ErrorMessage = "Максимальна довжина - 5 символів")]
    public string SizeSybmol { get; set; }

    [Required(ErrorMessage = "Поле обов'язкове для введення")]
    [MaxLength(10, ErrorMessage = "Максимальна довжина - 10 символів")]
    public double Price { get; set; }

    public Guid ProductId { get; set; }
}