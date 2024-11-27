using MauiProj.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiProj.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        //void UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<List<Tag>> GetTags();
        Task<Tag> AddTag(string name);
        Task<List<Category>> GetCategories();
        Task<Category> CreateCategory(string name);
        Task<bool> DeleteCategory(int id);

    }
 
}
