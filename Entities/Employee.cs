using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Employee
    {
        [StringLength(40)]
        public string? EmployeeName { get; set; }

        [Key]
        public Guid? EmployeeID {  get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(30)]
        public string? Position {  get; set; }

        [StringLength(40)]
        public string? Email { get; set; }
    }
}