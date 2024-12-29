using MAUI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace MAUI.Services
{
    public class TaskService : ITaskService
    {
        private const string base_url = "http://localhost:8080";

        public async Task<List<TaskModel>> GetAll()
        {
            HttpClient _httpClient = new HttpClient {BaseAddress = new Uri(base_url)};
            try 
            {
                var tasks = await _httpClient.GetFromJsonAsync<List<TaskModel>>("all");
                return tasks;
            } catch {
                return null;
            } 
        }

        public async Task<TaskModel> GetById(int id)
        {
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(base_url) };
            try
            {
                var task = await _httpClient.GetFromJsonAsync<TaskModel>($"id/{id}");
                return task;
            }
            catch
            {
                return null;
            }
                        
        }

        public async Task<TaskModel> Create(TaskModel task)
        {
            TaskModelCreate newTask = new TaskModelCreate()
            {
                name = task.Name,
                done = task.Done,
            };
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(base_url) };
            var content = new StringContent(JsonConvert.SerializeObject(newTask), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("", content);
            if (response.IsSuccessStatusCode)
            {
                return task;
            }
            else return null;
        }

        public async Task<TaskModel> Update(TaskModel task)
        {
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(base_url) };
            var content = new StringContent(JsonConvert.SerializeObject(task), Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PutAsync("", content);
                return task;
            }
            catch
            {
                return null;
            }
           
        }

        public async Task<bool> Delete(int id)
        {
            HttpClient _httpClient = new HttpClient { BaseAddress = new Uri(base_url) };
            try
            {
                var tasks = await _httpClient.DeleteAsync($"id/{id}");
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
