using System.ComponentModel.DataAnnotations;

namespace SAP_Assignment3.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        public string? EmployeeName { get; set; }
        [Required]
        public int EmployeeAge { get; set; }
        [Required]
        public double EmployeeSalary { get; set; }
    }
}
