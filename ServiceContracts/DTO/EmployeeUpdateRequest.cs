using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class EmployeeUpdateRequest
    {
        [Required(ErrorMessage ="Employee ID can't be blank")]
        public Guid? EmployeeID { get; set; }

        [Required(ErrorMessage = "Person Name can't be blank")]
        public string? EmployeeName { get; set; }


        public string? Position { get; set; }

        [Required(ErrorMessage = "Gender must be selected")]
        public GenderOptions? Gender { get; set; }


        [Required(ErrorMessage = "Email can't be blank")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }

        public Employee ToEmployee()
        {
            return new Employee()
            {
                EmployeeID = EmployeeID, EmployeeName = EmployeeName, Position = Position,
                Gender = Gender.ToString(), Email = Email
            };
        }

    }
}
