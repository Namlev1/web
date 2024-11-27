using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiProj.Services;
using System.Collections.ObjectModel;

namespace MauiProj.Models;

[QueryProperty(nameof(ProductsViewModel), nameof(ProductsViewModel))]
public partial class CategoryViewModel : ObservableObject
{

    public CategoryViewModel(IProductService productService)
    {
        _productService = productService;
        GetCategories();
    }

    [ObservableProperty]
    private ObservableCollection<Category> availableCategories;

    private async Task GetCategories()
    {
        var result = await _productService.GetCategories();
        AvailableCategories = new ObservableCollection<Category>(result);

    }

    private readonly IProductService _productService;
    private ProductsViewModel _productsViewModel;

    [ObservableProperty]
    private string newCategory;

    public ProductsViewModel ProductsViewModel
    {
        get { return _productsViewModel; }
        set { _productsViewModel = value; }
    }


    [RelayCommand]
    public async Task AddCategory()
    {
        if(NewCategory != null && NewCategory.Length > 0) await _productService.CreateCategory(NewCategory);
        GetCategories();
        //await Shell.Current.GoToAsync("../", true);
    }

    [RelayCommand]
    public async Task DeleteCategory(Category category)
    {
        await _productService.DeleteCategory(category.Id);
        GetCategories();
    }

    
}