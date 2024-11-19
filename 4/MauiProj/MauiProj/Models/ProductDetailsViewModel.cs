using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiProj.Services;

namespace MauiProj.Models;

[QueryProperty(nameof(Product), nameof(Product))]
[QueryProperty(nameof(ProductsViewModel), nameof(ProductsViewModel))]
public partial class ProductDetailsViewModel : ObservableObject
{

    private readonly IProductService _productService;
    private ProductsViewModel _productsViewModel;
    public ProductDetailsViewModel(IProductService productService)
	{
        _productService = productService;
    }

    public ProductsViewModel ProductsViewModel
    {
        get { return _productsViewModel; }
        set { _productsViewModel = value; }
    }

    [ObservableProperty]
    private Product product;

    [RelayCommand]
    public async Task Save()
    {
        if (_productService.GetProductById(product.Id) == null)
        {
            await CreateProduct();
        }
        else
        {
            await UpdateProduct();
        }

        await Shell.Current.GoToAsync("../", true);
    }

    public async Task CreateProduct()
    {
        _productService.AddProduct(product);
    }



    public async Task UpdateProduct()
    {
        _productService.UpdateProduct(product);
    }

    [RelayCommand]
    public async Task Delete()
    {
        _productService.DeleteProduct(product.Id);
        await Shell.Current.GoToAsync("../", true);
    }
}