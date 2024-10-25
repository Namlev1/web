using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private readonly JsonRepository<User> _userRepo = new JsonRepository<User>("Repo/users.json");
        private readonly JsonRepository<Book> _bookRepo = new JsonRepository<Book>("Repo/books.json");

        public IActionResult Index(string searchString, string sortOrder)
        {
            var users = _userRepo.GetAll();

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
            ViewData["BookCountSortParm"] = sortOrder == "bookCount" ? "bookCount_desc" : "bookCount";

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
                case "bookCount":
                    users = users.OrderBy(u => u.Books.Count).ToList();
                    break;
                case "bookCount_desc":
                    users = users.OrderByDescending(u => u.Books.Count).ToList();
                    break;
                default:
                    users = users.OrderBy(u => u.FirstName).ToList();
                    break;
            }

            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _userRepo.GetById(id);
            return user == null ? NotFound() : (IActionResult)View(user);
        }

        public IActionResult Create()
        {
            ViewBag.Books = _bookRepo.GetAll(); // Pass all books to the view
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user, int[] selectedBookIds)
        {
            if (ModelState.IsValid)
            {
                user.Books = _bookRepo.GetAll().Where(book => selectedBookIds.Contains(book.Id)).ToList();
                _userRepo.Add(user);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Books = _bookRepo.GetAll(); // Reload books if model state is invalid
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null) return NotFound();

            ViewBag.Books = _bookRepo.GetAll();
            ViewBag.SelectedBooks = user.Books.Select(b => b.Id).ToArray(); // Pre-select user's books
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user, int[] selectedBookIds)
        {
            if (ModelState.IsValid)
            {
                user.Books = _bookRepo.GetAll().Where(book => selectedBookIds.Contains(book.Id)).ToList();
                _userRepo.Update(user);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Books = _bookRepo.GetAll();
            ViewBag.SelectedBooks = selectedBookIds;
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
            return RedirectToAction("Index");
        }
    }
}