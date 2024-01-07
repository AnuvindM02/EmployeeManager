using ServiceContracts.DTO;

namespace EmployeeManager.ViewModels
{
    public class AddTaskModels
    {
        public List<TaskResponse> taskResponses = new List<TaskResponse>();
        public TaskAddRequest? taskAddRequest;
        public TaskUpdateRequest? taskUpdateRequest;
    }
}
