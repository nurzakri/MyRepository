using Microsoft.AspNetCore.Mvc;

namespace MovieProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
