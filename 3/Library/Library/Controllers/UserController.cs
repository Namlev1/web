using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private readonly JsonRepository<User> _userRepo = new JsonRepository<User>("Repo/users.json");
        private readonly JsonRepository<Book> _bookRepo = new JsonRepository<Book>("Repo/books.json");

        public IActionResult Index()
        {
            var users = _userRepo.GetAll();
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