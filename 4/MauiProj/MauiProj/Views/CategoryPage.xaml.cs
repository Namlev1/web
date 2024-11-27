using MauiProj.Models;
using Microsoft.Maui.Controls;
using MauiProj.Models;

namespace MauiProj.Views;

public partial class CategoryPage : ContentPage
{
	public CategoryPage(CategoryViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}