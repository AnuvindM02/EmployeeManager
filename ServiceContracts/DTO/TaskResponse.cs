using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
namespace ServiceContracts.DTO
{
    public class TaskResponse
    {
        public Guid? TaskID { get; set; }
        public Guid? EmployeeID { get; set; }
        public string? TaskDetails { get; set; }
        public bool IsCompleted { get; set; }
    }

    public static class TaskExtensions
    {
        public static TaskResponse ToTaskResponse(this EmployeeTask employeeTask)
        {
            return new TaskResponse() { TaskID = employeeTask.TaskID, EmployeeID = employeeTask.EmployeeID, TaskDetails = employeeTask.TaskDetails, IsCompleted = employeeTask.IsCompleted };
        }
    }
}
