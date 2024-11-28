using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiProj.Services;
using System.Collections.ObjectModel;

namespace MauiProj.Models;

[QueryProperty(nameof(Product), nameof(Product))]
[QueryProperty(nameof(ProductsViewModel), nameof(ProductsViewModel))]
public partial class ProductOverviewViewModel : ObservableObject
{

    public ProductOverviewViewModel(IProductService productService)
    {
        _productService = productService;
        GetCategories();
    }

    private readonly IProductService _productService;
    private ProductsViewModel _productsViewModel;

    public ProductsViewModel ProductsViewModel
    {
        get { return _productsViewModel; }
        set { _productsViewModel = value; }
    }

    [ObservableProperty]
    private ObservableCollection<Category> availableCategories; 

    //[ObservableProperty]
    //private ObservableCollection<Object> selectedTags = new();

    [ObservableProperty]
    private Product product;

    [ObservableProperty]
    private string newTag;

    private async Task GetCategories()
    {
        var result = await _productService.GetCategories();
        AvailableCategories = new ObservableCollection<Category>(result);

    }



    [RelayCommand]
    public async Task Save()
    {
        if (product.Category == null)
        {
            product.Category = AvailableCategories[0];
        }

        Product p = await _productService.GetProductById(product.Id);
        if (p == null)
        {
            await CreateProduct();
        }
        else
        {
            await UpdateProduct();
            //await UpdateProduct(); //TODO: dodaæ update produktu
        }

        await ProductsViewModel.GetProducts();
        await Shell.Current.GoToAsync("../", true);
    }

    public async Task CreateProduct()
    {
        await _productService.CreateProduct(product);
    }



    public async Task UpdateProduct()
    {
        await _productService.UpdateProduct(product);
    }

    [RelayCommand]
    public async Task Delete()
    {
        var result = await _productService.DeleteProduct(product.Id);
        await ProductsViewModel.GetProducts();
        await Shell.Current.GoToAsync("../", true);
    }

    [RelayCommand]
    public async void AddTag()
    {
        
        if (newTag != null &&!product.Tags.Any(x => x.Name == newTag) && newTag.Length > 0)
        {
            var result = await _productService.AddTag(NewTag);
            product.Tags.Add(result);
        }
        NewTag = null;
    }

    [RelayCommand]
    public async void RemoveTag(Tag tag)
    {
        product.Tags.Remove(tag);
    }


}