using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using mvc.Models;

public class TaskController : Controller
{
    private readonly HttpClient _httpClient;

    public TaskController()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://10.10.0.16:8080/api/task/")
        };
    }

    // Akcja do wyświetlania wszystkich zadań
    public async Task<IActionResult> Index()
    {
        var tasks = await _httpClient.GetFromJsonAsync<List<TaskModel>>("all");
        return View(tasks);
    }

    // Akcja do usuwania zadania
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"id/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }
        return StatusCode((int)response.StatusCode);
    }

    // GET: Formularz do tworzenia nowego zadania
    public IActionResult Create()
    {
        return View();
    }

    // POST: Tworzenie nowego zadania
    [HttpPost]
    public async Task<IActionResult> Create(TaskModel task)
    {
        if (!ModelState.IsValid)
        {
            return View(task);
        }

        var json = JsonSerializer.Serialize(task);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("", content);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Failed to create a new task");
        return View(task);
    }
    
    // GET: Edit task
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _httpClient.GetFromJsonAsync<TaskModel>($"id/{id}");
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }
    
        // POST: Update task
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskModel task)
        {
            if (!ModelState.IsValid)
            {
                return View(task);
            }
    
            // Add the ID to the task to ensure it's sent for the correct task
            task.id = id;
    
            var json = JsonSerializer.Serialize(task);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
    
            var response = await _httpClient.PutAsync("", content);
    
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
    
            ModelState.AddModelError(string.Empty, "Failed to update task");
            return View(task);
        }
}
