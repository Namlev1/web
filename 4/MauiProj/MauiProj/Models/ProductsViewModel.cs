using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiProj.Services;
using MauiProj.Views;
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
        private int _idCounter = 0;

        [ObservableProperty]
        public ObservableCollection<Product> _products;

        [ObservableProperty]
        private Product _selectedProduct;


        public ProductsViewModel(IProductService productService)
        {
            _productService = productService;
            Products = _productService.GetProducts();
        }

        
        [RelayCommand]
        public async Task AddProduct()
        {
            _idCounter ++;
            SelectedProduct = new Product { Id = _idCounter};

          
            await Shell.Current.GoToAsync(nameof(ProductDetailsPage),  new Dictionary<string, object>
            {
                {"Product",SelectedProduct },
                {nameof(ProductsViewModel), this }
            });
            //var newProduct = new Product { Id = _products.Count + 1, Name = "New Product", Description = "Description", Price = 0.0m };
            //_productService.AddProduct(newProduct);

        }

        [RelayCommand]
        public async Task Details(Product p)
        {
            SelectedProduct = p;
            await Shell.Current.GoToAsync(nameof(ProductDetailsPage), new Dictionary<string, object>
            {
                {"Product",SelectedProduct },
                {nameof(ProductsViewModel), this }
            });
        }

        [RelayCommand]
        public async Task Delete(Product p)
        {
            _productService.DeleteProduct(p.Id);
        }
    }

}
