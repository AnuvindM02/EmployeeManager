using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ITaskServices
    {
        /// <summary>
        /// Add new task
        /// </summary>
        /// <param name="taskAddRequest"></param>
        /// <returns>TaskResponse class</returns>
        TaskResponse AddTask(TaskAddRequest? taskAddRequest, Guid EmployeeID);

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <param name="taskAddRequest"></param>
        /// <returns>List of TaskResponse</returns>
        List<TaskResponse> GetAllTasks();

        /// <summary>
        /// Complete the task
        /// </summary>
        /// <returns></returns>
        TaskResponse UpdateTask(TaskUpdateRequest? taskUpdateRequest);

        /// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        bool DeleteTask(Guid? taskId);

        /// <summary>
        /// Get all tasks of selected employee
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        List<TaskResponse> GetTasksByEmployeeID(Guid? EmployeeID);

        /// <summary>
        /// Get task by task id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns>TaskResponse class</returns>
        TaskResponse? GetTaskById(Guid? taskId);

    }
}
