using Microsoft.AspNetCore.Mvc;
using CalculatorApp.Models;
using System.Collections.Generic;

namespace CalculatorApp.Controllers
{
    public class TodoController : Controller
    {
        private static List<TodoModel> todos = new List<TodoModel>();
        private static int nextId = 1;

        // GET: /Todo/
        public IActionResult Index()
        {
            return View(todos);
        }

        // POST: /Todo/
        [HttpPost]
        public IActionResult Index(TodoModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = nextId++;
                todos.Add(model);
            }

            return View(todos);
        }

        // POST: /Todo/Complete
        [HttpPost]
        public IActionResult Complete(int id)
        {
            var todo = todos.Find(t => t.Id == id);
            if (todo != null)
            {
                todo.IsCompleted = true;
            }
            return RedirectToAction("Index");
        }

        // POST: /Todo/Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var todo = todos.Find(t => t.Id == id);
            if (todo != null)
            {
                todos.Remove(todo);
            }
            return RedirectToAction("Index");
        }
    }
}
