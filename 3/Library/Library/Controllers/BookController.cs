using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly JsonRepository<Book> _bookRepo = new JsonRepository<Book>("Repo/books.json");
        private readonly JsonRepository<User> _userRepo = new JsonRepository<User>("Repo/users.json");

        public IActionResult Index(string searchString, string sortOrder)
        {
            var books = _bookRepo.GetAll();

            // Filtering
            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                         b.ISBN.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                         b.Author.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                         b.Genre.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                                         b.Id.ToString().Contains(searchString)).ToList();
            }

            // Sorting
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["AuthorSortParm"] = sortOrder == "author" ? "author_desc" : "author";
            ViewData["GenreSortParm"] = sortOrder == "genre" ? "genre_desc" : "genre";
            ViewData["ReleaseDateSortParm"] = sortOrder == "releaseDate" ? "releaseDate_desc" : "releaseDate";

            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(b => b.Title).ToList();
                    break;
                case "author":
                    books = books.OrderBy(b => b.Author).ToList();
                    break;
                case "author_desc":
                    books = books.OrderByDescending(b => b.Author).ToList();
                    break;
                case "genre":
                    books = books.OrderBy(b => b.Genre).ToList();
                    break;
                case "genre_desc":
                    books = books.OrderByDescending(b => b.Genre).ToList();
                    break;
                case "releaseDate":
                    books = books.OrderBy(b => b.ReleaseDate).ToList();
                    break;
                case "releaseDate_desc":
                    books = books.OrderByDescending(b => b.ReleaseDate).ToList();
                    break;
                default:
                    books = books.OrderBy(b => b.Title).ToList();
                    break;
            }

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _bookRepo.GetById(id);
            return book == null ? NotFound() : (IActionResult)View(book);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepo.Add(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = _bookRepo.GetById(id);
            return book == null ? NotFound() : (IActionResult)View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepo.Update(book);
                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = _bookRepo.GetById(id);
            return book == null ? NotFound() : (IActionResult)View(book);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _bookRepo.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            // Remove book from all users
            var users = _userRepo.GetAll();
            foreach (var user in users)
            {
                user.Books.RemoveAll(b => b.Id == id);
            }

            // Save updated users
            _userRepo.SaveData(users);

            // Delete the book
            _bookRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}