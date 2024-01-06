using Microsoft.AspNetCore.Mvc;

namespace TasksApp.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("[action]")]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
