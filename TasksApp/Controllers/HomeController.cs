using EmployeeManager.ViewModels;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace TasksApp.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {

        private readonly IEmployeeServices _employeeServices;
        private readonly ITaskServices _taskServices;

        public HomeController(IEmployeeServices employeeServices, ITaskServices taskServices)
        {
            _employeeServices = employeeServices;
            _taskServices = taskServices;
        }


        [Route("[action]")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(EmployeeResponse.EmployeeName),SortOrderOptions sortOrder = SortOrderOptions.ASC)
        {

            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                {nameof(EmployeeResponse.EmployeeName), "Employee Name"},
                {nameof(EmployeeResponse.Position), "Position" },
                {nameof(EmployeeResponse.Gender), "Gender" },
                {nameof(EmployeeResponse.Email), "Email" }
            };


            //Filtering
            List<EmployeeResponse> employees = _employeeServices.GetFilteredEmployees(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            //Sorting
            List<EmployeeResponse> sortedEmployees = _employeeServices.GetSortedEmployees(employees, sortBy, sortOrder);
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder.ToString();

            return View(sortedEmployees);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Create(EmployeeAddRequest employeeAddRequest)
        {
            //Checking for any errors
            if(!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return View();
            }

            //Calling the service method
            EmployeeResponse employeeResponse = _employeeServices.AddEmployee(employeeAddRequest);
            
            //Returning to the home page
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        [Route("[action]/{EmployeeID}")]
        public IActionResult Delete(Guid? EmployeeID)
        {
            EmployeeResponse? employeeResponse = _employeeServices.GetEmployeeById(EmployeeID);
            if(employeeResponse == null)
                return RedirectToAction("Index","Home");

            return View(employeeResponse);
        }

        [HttpPost]
        [Route("[action]/{EmployeeID}")]
        public IActionResult Delete(EmployeeUpdateRequest employeeUpdateRequest)
        {
            EmployeeResponse? employeeResponse = _employeeServices.GetEmployeeById(employeeUpdateRequest.EmployeeID);
            if (employeeResponse == null)
                return RedirectToAction("Index", "Home");

            _employeeServices.DeleteEmployee(employeeUpdateRequest.EmployeeID);
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        [Route("[action]/{EmployeeID}")]
        public IActionResult Edit(Guid? EmployeeID)
        {
            EmployeeResponse? employeeResponse = _employeeServices.GetEmployeeById(EmployeeID);
            if (employeeResponse == null)
                return RedirectToAction("Index", "Home");

            EmployeeUpdateRequest employeeUpdateRequest = employeeResponse.ToEmployeeUpdateRequest();

            return View(employeeUpdateRequest);
        }

        [HttpPost]
        [Route("[action]/{EmployeeID}")]
        public IActionResult Edit(EmployeeUpdateRequest employeeUpdateRequest)
        {
            EmployeeResponse? employeeResponse = _employeeServices.GetEmployeeById(employeeUpdateRequest.EmployeeID);

            if (employeeResponse == null)
                return RedirectToAction("Index", "Home");

            if(ModelState.IsValid)
            {
                EmployeeResponse updatedEmployee = _employeeServices.UpdateEmployee(employeeUpdateRequest);
                return RedirectToAction("Index");
            }

            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return View(employeeResponse.ToEmployeeUpdateRequest());
        }

        [HttpGet]
        [Route("[action]/{EmployeeID}")]
        public IActionResult AddTask(Guid? EmployeeID)
        {
            AddTaskModels addTaskModels = new AddTaskModels();

            List<TaskResponse> taskResponses=_taskServices.GetTasksByEmployeeID(EmployeeID);

            addTaskModels.taskResponses = taskResponses;

            ViewBag.EmployeeName = _employeeServices.GetEmployeeById(EmployeeID).EmployeeName;
            ViewBag.EmployeeID = EmployeeID.ToString();

            return View(addTaskModels);
        }

        [HttpPost]
        [Route("[action]/{EmployeeID}")]
        public IActionResult AddTask(TaskAddRequest taskAddRequest,Guid EmployeeID)
        {
            //Checking for any errors
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return RedirectToAction("AddTask", new { EmployeeID = EmployeeID });
            }

            //Calling the service method
            TaskResponse taskResponse = _taskServices.AddTask(taskAddRequest,EmployeeID);

            //Staying on the same page
            return RedirectToAction("AddTask", new { EmployeeID = EmployeeID });
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EditTaskStatus(TaskUpdateRequest? taskUpdateRequest)
        {
            TaskResponse? taskResponse = _taskServices.GetTaskById(taskUpdateRequest?.TaskID);

            if (taskResponse == null)
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                TaskResponse updatedTask = _taskServices.UpdateTask(taskUpdateRequest);
                return RedirectToAction("AddTask", new { EmployeeID = taskUpdateRequest.EmployeeID });
            }

            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return RedirectToAction("AddTask", new { EmployeeID = taskUpdateRequest.EmployeeID });
        }

        /*[Route("[action]")]
        public IActionResult AllTasks()
        {
            return View();
        }*/
        
    }
}
