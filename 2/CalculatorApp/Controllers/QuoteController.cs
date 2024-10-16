using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApp.Controllers
{
    public class QuoteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
