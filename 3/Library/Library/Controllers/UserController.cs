using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Library.Models;

namespace Library.Controllers
{
    public class UserController : Controller
    {
        private readonly JsonRepository<User> _userRepo = new JsonRepository<User>("path_to_users.json");

        public IActionResult Index()
        {
            var users = _userRepo.GetAll();
            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _userRepo.GetById(id);
            return View(user);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(User user)
        {
            _userRepo.Add(user);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var user = _userRepo.GetById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            _userRepo.Update(user);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _userRepo.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
