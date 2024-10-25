using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private readonly JsonRepository<User> _userRepo = new JsonRepository<User>("Repo/users.json");

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

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepo.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        public IActionResult Edit(int id)
        {
            var user = _userRepo.GetById(id);
            return user == null ? NotFound() : (IActionResult)View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepo.Update(user);
                return RedirectToAction(nameof(Index));
            }
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
