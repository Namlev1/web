using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiProj.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiProj.Models
{
    public partial class ProductsViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        public ObservableCollection<Product> Products => _productService.GetProducts();


        public ProductsViewModel(IProductService productService)
        {
            _productService = productService;
        }

        
        [RelayCommand]
        private void AddProduct()
        {
            var newProduct = new Product { Id = Products.Count + 1, Name = "New Product", Description = "Description", Price = 0.0m };
            _productService.AddProduct(newProduct);
            
        }
    }

}
