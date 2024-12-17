using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers;

public class TaskController : Controller
{
    private readonly HttpClient _httpClient;

    public TaskController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:8080/api/task/");
    }

    public async Task<IActionResult> Index()
    {
        var tasks = new List<TaskModel>();

        try
        {
            // Fetch data from REST API
            var response = await _httpClient.GetAsync("all");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                tasks = JsonSerializer.Deserialize<List<TaskModel>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        catch (Exception ex)
        {
            // Log the error or pass an error message
            ViewBag.Error = "Error fetching tasks: " + ex.Message;
        }

        return View(tasks);
    }

    // Akcja DELETE
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _httpClient.DeleteAsync($"id/{id}");
        if (response.IsSuccessStatusCode) return RedirectToAction("Index");
        return StatusCode((int)response.StatusCode);
    }
}