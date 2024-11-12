using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private readonly JsonRepository<User> _userRepo = new JsonRepository<User>("Repo/users.json");
        private readonly JsonRepository<Book> _bookRepo = new JsonRepository<Book>("Repo/books.json");
        private readonly JsonRepository<Loan> _loanRepo = new JsonRepository<Loan>("Repo/loans.json");

        public IActionResult Index(string searchString, string sortOrder)
        {
            var users = _userRepo.GetAll();
            var loans = _loanRepo.GetAll();

            // Filtering
            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => u.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                         u.SecondName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                         u.Id.ToString().Contains(searchString)).ToList();
            }

            // Sorting
            ViewData["FirstNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fname_desc" : "";
            ViewData["SecondNameSortParm"] = sortOrder == "sname" ? "sname_desc" : "sname";
            ViewData["LoanCountSortParm"] = sortOrder == "loanCount" ? "loanCount_desc" : "loanCount";

            switch (sortOrder)
            {
                case "fname_desc":
                    users = users.OrderByDescending(u => u.FirstName).ToList();
                    break;
                case "sname":
                    users = users.OrderBy(u => u.SecondName).ToList();
                    break;
                case "sname_desc":
                    users = users.OrderByDescending(u => u.SecondName).ToList();
                    break;
                case "loanCount":
                    users = users.OrderBy(u => loans.Count(l => l.UserId == u.Id)).ToList(); // TODO: chech if this works
                    break;
                case "loanCount_desc":
                    users = users.OrderByDescending(u => loans.Count(l => l.UserId == u.Id)).ToList(); // TODO: chech if this works
                    break;
                default:
                    users = users.OrderBy(u => u.FirstName).ToList();
                    break;
            }

            // count book loans for each user
            Dictionary<int, int> BookLoansPerUser = loans.Where(l => !l.IsReturned).GroupBy(l => l.UserId).ToDictionary(g => g.Key, g =>  g.Count());
            ViewBag.BookLoans = BookLoansPerUser;

            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _userRepo.GetById(id);
            return user == null ? NotFound() : View(user);
        }

        public IActionResult Create()
        {
            //ViewBag.Books = _bookRepo.GetAll(); // Pass all books to the view
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)//, int[] selectedBookIds)
        {
            if (ModelState.IsValid)
            {
                //user.Books = _bookRepo.GetAll().Where(book => selectedBookIds.Contains(book.Id)).ToList();
                _userRepo.Add(user);
                return RedirectToAction(nameof(Index));
            }

            //ViewBag.Books = _bookRepo.GetAll(); // Reload books if model state is invalid
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null) return NotFound();

            //ViewBag.Books = _bookRepo.GetAll();
            //ViewBag.SelectedBooks = user.Books.Select(b => b.Id).ToArray(); // Pre-select user's books
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)//, int[] selectedBookIds)
        {
            if (ModelState.IsValid)
            {
                //user.Books = _bookRepo.GetAll().Where(book => selectedBookIds.Contains(book.Id)).ToList();
                _userRepo.Update(user);
                return RedirectToAction(nameof(Index));
            }
            
            //ViewBag.Books = _bookRepo.GetAll();
            //ViewBag.SelectedBooks = selectedBookIds;
            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = _userRepo.GetById(id);
            return user == null ? NotFound() : (IActionResult)View(user);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            _userRepo.Delete(id);

            var loans = _loanRepo.GetAll();
            loans.RemoveAll(l => l.UserId == id);
            _loanRepo.SaveData(loans);

            return RedirectToAction("Index");
        }

        public IActionResult NewLoan(int id)
        {
            var user = _userRepo.GetById(id);
            ViewData["userId"] = id;

            var books = _bookRepo.GetAll();
            Dictionary<int, string> BookId_BookTitle = books.ToDictionary(b => b.Id, b => b.Title);
            ViewBag.BookNames = BookId_BookTitle;

            return user == null ? NotFound() : View(user);
        }

        [HttpPost]
        public IActionResult NewLoan(Loan loan)//, int[] selectedBookIds)
        {
            if (ModelState.IsValid)
            {
                _loanRepo.Add(loan);
                return RedirectToAction(nameof(Index));
            }

            if (ViewData["userId"] == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return NewLoan((int)ViewData["userId"]);
        }

        public IActionResult UserLoanInfo(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null) return NotFound();
            ViewData["userId"] = id;
            ViewData["userFName"] = user.FirstName;
            ViewData["userSName"] = user.SecondName;

            var books = _bookRepo.GetAll();
            Dictionary<int, string> BookId_BookTitle = books.ToDictionary(b => b.Id, b => b.Title);
            ViewBag.BookNames = BookId_BookTitle;

            var loans = _loanRepo.GetAll();
            loans = loans.Where(l => l.UserId == id && !l.IsReturned).OrderBy(l => l.DueDate).ToList();

            return View(loans);
        }

        public IActionResult CloseLoan(int id)
        {
            var Loan = _loanRepo.GetById(id);
            if (Loan == null) return NotFound();

            Loan.IsReturned = true;
            _loanRepo.Update(Loan);

            
            return RedirectToAction(nameof(Index));
        }
    }
}