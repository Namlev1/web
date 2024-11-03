using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Define the path to the JSON file.
        private readonly string _jsonFilePathLoans = Path.Combine(Directory.GetCurrentDirectory(), "Repo", "loans.json");
		private readonly string _jsonFilePathBooks = Path.Combine(Directory.GetCurrentDirectory(), "Repo", "books.json");

		[HttpGet]
        [Route("api/data/loans")]
        public async Task<IActionResult> GetLoansData()
        {
            if (!System.IO.File.Exists(_jsonFilePathLoans))
            {
                return NotFound("Loans data file not found.");
            }

            // Read the file content
            var jsonContent = await System.IO.File.ReadAllTextAsync(_jsonFilePathLoans);

            // Return as JSON response
            return Content(jsonContent, "application/json");
        }

		[HttpGet]
		[Route("api/data/books")]
		public async Task<IActionResult> GetBooksData()
		{
			if (!System.IO.File.Exists(_jsonFilePathBooks))
			{
				return NotFound("Books data file not found.");
			}

			// Read the file content
			var jsonContent = await System.IO.File.ReadAllTextAsync(_jsonFilePathBooks);

			// Return as JSON response
			return Content(jsonContent, "application/json");
		}
	}
}
