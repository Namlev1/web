using Microsoft.AspNetCore.Mvc;
using CalculatorApp.Models;

namespace CalculatorApp.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: /Calculator/
        public IActionResult Index()
        {
            var model = new CalculatorModel();
            return View(model);
        }

        // POST: /Calculator/
        [HttpPost]
        public IActionResult Index(CalculatorModel model)
        {
            if (ModelState.IsValid)
            {
                // Dodaj liczby i ustaw wynik
                model.Result = model.Number1 + model.Number2;
            }

            // Przekaż model z wynikiem do widoku
            return View(model);
        }
    }
}