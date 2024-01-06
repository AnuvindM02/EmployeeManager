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
        public IActionResult Index()
        {            
            List<EmployeeResponse> employees = _employeeServices.GetAllEmployees();

            return View(employees);
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

        
    }
}
