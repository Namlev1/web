using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
