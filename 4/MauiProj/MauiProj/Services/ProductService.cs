using MauiProj.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace MauiProj.Services
{
    public class ProductService : IProductService
    {
        private const string base_url = "http://localhost:8080";

        public async Task<List<Product>> GetProducts()
        {
            string url = base_url + "/api/product/all";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Product>>(json);
                return result;
            }
        }

        public async Task<List<Tag>> GetTags()
        {
            string url = base_url + "/api/tag/all";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Tag>>(json);
                return result;
            }
        }
            

        public async Task<List<Category>> GetCategories()
        {
            string url = base_url + "/api/category/all";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<Category>>(json);
                return result;
            }
        }

        public async Task<Product> CreateProduct(Product product)
        {
            ProductPost post = new ProductPost();
            post.name = product.Name;
            post.description = product.Description;
            post.price = product.Price;
            post.details.manufacturer = product.Details.Manufacturer;
            post.details.warranty = product.Details.Warranty;
            post.category.id = product.Category.Id;
            post.tags = product.Tags == null ? [] : product.Tags.Select(x => new TagPost { id = x.Id }).ToArray();

            string url = base_url + "/api/product/";
            using (HttpClient client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
                //string postJson = JsonConvert.SerializeObject(post);
                var response = await client.PostAsync(url, content);
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Product>(json);
                return result;
            }
        }

        //public void UpdateProduct(Product product)
        //{
        //    var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        //    if (existingProduct != null)
        //    {
        //        existingProduct.Name = product.Name;
        //        existingProduct.Description = product.Description;
        //        existingProduct.Price = product.Price;
        //    }
        //}

        public async Task<bool> DeleteProduct(int id)
        {
            string url = base_url + "/api/product/id/" + id.ToString();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                if (json == "") return true; // <-- zmienić to
                return false;
            }
        }

        public async Task<Category> CreateCategory(string name)
        {
            string url = base_url + "/api/category/";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(url, new {name=name});
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Category>(json);
                return result;
            }
        }

        public async Task<Tag> GetTagByName(string name)
        {
            string url = base_url + "/api/tag/name/" + name;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Tag>(json);
                return result;
            }
        }

        public async Task<Tag> AddTag(string name)
        {
            Tag tag = await GetTagByName(name);
            if (tag == null)
            {
                string url = base_url + "/api/tag/";
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsJsonAsync(url, new { name = name });
                    string json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Tag>(json);
                    return result;
                }
            }
            else
            {
                return tag;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            string url = base_url + "/api/product/id/" + id.ToString();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Product>(json);
                return result;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            string url = base_url + "/api/category/id/" + id.ToString();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                if (json == "") return true;
                return false;
            }
        }
    }



    class ProductPost
    {
        public string name;
        public string description;
        public decimal price;
        public ProductDetailsPost details = new ProductDetailsPost();
        public CategoryPost category = new CategoryPost();
        public TagPost[] tags;

    }

    class CategoryPost
    {
        public int id;
    }

    class ProductDetailsPost
    {
        public string manufacturer;
        public string warranty;
    }

    class TagPost
    {
        public int id;
    }



}
