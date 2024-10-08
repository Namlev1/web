using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt1.Pages
{
    public class UserFormModel : PageModel
    {
        [BindProperty]
        public UserInputModel UserInput { get; set; }

        public bool IsSubmitted { get; private set; }

        public void OnGet()
        {
            IsSubmitted = false;
        }

        public IActionResult OnPost()
        {
            // Process the form data without validation
            IsSubmitted = true;

            // Stay on the same page and display the data
            return Page();
        }
    }
}
