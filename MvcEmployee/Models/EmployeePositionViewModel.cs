using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcEmployee.Models;

public class EmployeePositionViewModel
{
    public List<Employee>? Employees { get; set; }
    public SelectList? Positions { get; set; }
    public string? EmployeePosition { get; set; }
    public string? SearchString { get; set; }
}