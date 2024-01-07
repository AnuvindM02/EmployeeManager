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

        public HomeController(IEmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
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
    }
}
