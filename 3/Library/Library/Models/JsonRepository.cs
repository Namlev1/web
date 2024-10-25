using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Newtonsoft.Json;

namespace Library.Models
{
    public class JsonRepository<T> where T : class
    {
        private readonly string _filePath;

        public JsonRepository(string filePath)
        {
            _filePath = filePath;
        }

        private List<T> LoadData()
        {
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");

            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
        }

        private void SaveData(List<T> data)
        {
            var jsonData = JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(_filePath, jsonData);
        }


        public List<T> GetAll() => LoadData();

        public T GetById(int id) =>
            LoadData().FirstOrDefault(item => (int)item.GetType().GetProperty("Id")!.GetValue(item)! == id);

        public void Add(T item)
        {
            var data = LoadData();

            // Determine the next ID
            var maxId = data.Any() ? data.Max(x => (int)x.GetType().GetProperty("Id")!.GetValue(x)!) : 0;
            item.GetType().GetProperty("Id")!.SetValue(item, maxId + 1);

            data.Add(item);
            SaveData(data);
        }

        public void Update(T item)
        {
            var data = LoadData();
            var index = data.FindIndex(x =>
                (int)x.GetType().GetProperty("Id")!.GetValue(x)! ==
                (int)item.GetType().GetProperty("Id")!.GetValue(item)!);
            if (index >= 0)
            {
                data[index] = item;
                SaveData(data);
            }
        }

        public void Delete(int id)
        {
            var data = LoadData();
            var item = data.FirstOrDefault(x => (int)x.GetType().GetProperty("Id")!.GetValue(x)! == id);
            if (item != null)
            {
                data.Remove(item);
                SaveData(data);
            }
        }
    }
}