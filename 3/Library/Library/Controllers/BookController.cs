using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly JsonRepository<Book> _bookRepo = new JsonRepository<Book>("Repo/books.json");

        public IActionResult Index()
        {
            var books = _bookRepo.GetAll();
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
            _bookRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
