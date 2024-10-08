using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt1.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserInputModel UserInput { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Tutaj możesz dodać logikę rejestracji, np. zapis do bazy danych

            return RedirectToPage("Success");
        }
    }
}
