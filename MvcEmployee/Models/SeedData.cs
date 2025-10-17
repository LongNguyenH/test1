using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcEmployee.Data;
using System;
using System.Linq;

namespace MvcEmployee.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcEmployeeContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcEmployeeContext>>()))
        {
            // Look for any Employees.
            if (context.Employee.Any())
            {
                return;   // DB has been seeded
            }
            context.Employee.AddRange(
                new Employee
                {
                    Name = "Nhan vien 1",
                    DateofBirth = DateTime.Parse("1989-2-12"),
                    Position = "Manager",
                    Salary = 9.99M
                },
                new Employee
                {
                    Name = "Nhan vien 2",
                    DateofBirth = DateTime.Parse("1984-3-13"),
                    Position = "Vice Manager",
                    Salary = 8.99M
                },
                new Employee
                {
                    Name = "Nhan vien 3",
                    DateofBirth = DateTime.Parse("1986-2-23"),
                    Position = "Employee",
                    Salary = 4.99M
                },
                new Employee
                {
                    Name = "Nhan vien 4",
                    DateofBirth = DateTime.Parse("1959-4-15"),
                    Position = "Employee",
                    Salary = 3.99M
                }
            );
            context.SaveChanges();
        }
    }
}