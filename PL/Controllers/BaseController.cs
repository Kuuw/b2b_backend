using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
