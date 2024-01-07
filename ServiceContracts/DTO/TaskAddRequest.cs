using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
    public class TaskAddRequest
    {

        [Required(ErrorMessage = "Task cannot be empty")]
        public string? TaskDetails { get; set; }

        public bool IsCompleted { get; set; }

        public EmployeeTask ToEmployeeTask()
        {
            return new EmployeeTask() { TaskDetails = this.TaskDetails, IsCompleted = IsCompleted };
        }
    }
}
