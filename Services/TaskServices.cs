using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TaskServices : ITaskServices
    {

        private readonly EmployeesDbContext _db;

        public TaskServices(EmployeesDbContext employeesDbContext)
        {
            _db=employeesDbContext;
        }

        public TaskResponse AddTask(TaskAddRequest? taskAddRequest, Guid EmployeeID)
        {
            if(taskAddRequest == null) throw new ArgumentNullException(nameof(taskAddRequest));

            ValidationHelper.ModelValidation(taskAddRequest);

            EmployeeTask employeeTask = taskAddRequest.ToEmployeeTask();

            employeeTask.TaskID = Guid.NewGuid();
            employeeTask.EmployeeID = EmployeeID;

            _db.EmployeeTasks.Add(employeeTask);
            _db.SaveChanges();

            return employeeTask.ToTaskResponse();
        }

        public bool DeleteTask(Guid? taskId)
        {
            if(taskId == null) throw new ArgumentNullException(nameof(taskId));

            EmployeeTask? employeeTask = _db.EmployeeTasks.FirstOrDefault(emptask => emptask.TaskID == taskId);

            if (employeeTask == null)
                return false;

            _db.EmployeeTasks.Remove(_db.EmployeeTasks.First(emptask => emptask.TaskID == taskId));
            _db.SaveChanges();
            return true;
        }

        public List<TaskResponse> GetAllTasks()
        {
            return _db.EmployeeTasks.Select(emptask => emptask.ToTaskResponse()).ToList();
        }

        public TaskResponse? GetTaskById(Guid? taskId)
        {
            if (taskId == null)
                return null;

            EmployeeTask? employeeTask = _db.EmployeeTasks.FirstOrDefault(emptask => emptask.TaskID == taskId);

            if (employeeTask == null)
                return null;

            return employeeTask.ToTaskResponse();
        }

        public List<TaskResponse>? GetTasksByEmployeeID(Guid? EmployeeID)
        {
            if (EmployeeID == null)
                return null;

            List<TaskResponse> employeeTasks = _db.EmployeeTasks.Where(emptask => emptask.EmployeeID == EmployeeID).
                Select(emptask => emptask.ToTaskResponse()).ToList();

            return employeeTasks;
        }

        public TaskResponse UpdateTask(TaskUpdateRequest? taskUpdateRequest)
        {
            if(taskUpdateRequest == null)
                throw new ArgumentNullException(nameof(taskUpdateRequest));

            ValidationHelper.ModelValidation(taskUpdateRequest);

            EmployeeTask? matchingEmployeeTask = _db.EmployeeTasks.FirstOrDefault(emptask => emptask.TaskID == taskUpdateRequest.TaskID);

            if (matchingEmployeeTask == null)
                throw new ArgumentException("Given task id doesn't exist");
            
            //Updation
            matchingEmployeeTask.TaskDetails = taskUpdateRequest.TaskDetails;
            matchingEmployeeTask.IsCompleted = taskUpdateRequest.IsCompleted;
            _db.SaveChanges();
            
            return matchingEmployeeTask.ToTaskResponse();
        }
    }
}
