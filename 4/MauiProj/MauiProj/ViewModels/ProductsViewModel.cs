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

        //[ObservableProperty]
        //private ObservableCollection<Tag> _availableTags;

        [ObservableProperty]
        private ObservableCollection<Category> _availableCategories;


        public ProductsViewModel(IProductService productService)
        {
            _productService = productService;
            GetProducts();
            //_availableTags = productService.GetTags();
            //_availableCategories = productService.GetCategories();
        }

        public async Task GetProducts()
        {
            var result = await _productService.GetProducts();
            Products = new ObservableCollection<Product>(result);
        }

        
        [RelayCommand]
        public async Task AddProduct()
        {

            SelectedProduct = new Product {Id=-1};

          
            await Shell.Current.GoToAsync(nameof(ProductOverviewPage),  new Dictionary<string, object>
            {
                {"Product",SelectedProduct },
                {nameof(ProductsViewModel), this }
            });

        }

        [RelayCommand]
        public async Task AddCategory()
        {

            SelectedProduct = new Product { Id = _idCounter };


            await Shell.Current.GoToAsync(nameof(CategoryPage), new Dictionary<string, object>
            {
                {nameof(ProductsViewModel), this }
            });

        }

        [RelayCommand]
        public async Task Details(Product p)
        {
            SelectedProduct = p;
            await Shell.Current.GoToAsync(nameof(ProductOverviewPage), new Dictionary<string, object>
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

        [RelayCommand]
        public async void Refresh()
        {
            GetProducts();
        }
    }

}
