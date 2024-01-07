using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class TaskUpdateRequest
    {
        [Required(ErrorMessage = "TaskID cannot be blank")]
        public Guid? TaskID { get; set; }

        [Required(ErrorMessage = "EmployeeID cannot be blank")]
        public Guid? EmployeeID { get; set; }

        [Required(ErrorMessage = "Task cannot be empty")]
        public string? TaskDetails { get; set; }

        public bool IsCompleted { get; set; }

        public EmployeeTask ToEmployeeTask()
        {
            return new EmployeeTask() { EmployeeID = EmployeeID, TaskID = TaskID, TaskDetails = TaskDetails, IsCompleted = IsCompleted };
        }
    }
}
