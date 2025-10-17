using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcEmployee.Models;

public class Employee
{
    public int Id { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required]
    [Display(Name = "Tên")]
    public string? Name { get; set; }

    [Display(Name = "Ngày sinh")]
    
    [DataType(DataType.Date)]
    public DateTime DateofBirth { get; set; }
    [Required]
    [StringLength(30)]
    [Display(Name = "Chức vụ")]
    public string? Position { get; set; }

    [Range(1, 100)]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    [Display(Name = "Lương")]
    public decimal Salary { get; set; }
}
