using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using ServiceContracts.Enums;

namespace ServiceContracts.DTO
{
    public class EmployeeAddRequest
    {
        [Required(ErrorMessage ="Person Name can't be blank")]
        public string? EmployeeName {  get; set; }
       

        public string? Position { get; set; }

        [Required(ErrorMessage ="Gender must be selected")]
        public GenderOptions? Gender { get; set; }


        [Required(ErrorMessage ="Email can't be blank")]
        [EmailAddress(ErrorMessage ="Invalid Email")]
        public string? Email { get; set; }

        public Employee ToEmployee()
        {
            return new Employee() { EmployeeName = EmployeeName, Email = Email, Gender = Gender.ToString(), Position = Position };
        }
    }

}
