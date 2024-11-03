using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class LoanController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
