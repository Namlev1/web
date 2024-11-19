using MauiProj.Models;
using Microsoft.Maui.Controls;
using MauiProj.Models;

namespace MauiProj.Views;

public partial class ProductDetailsPage : ContentPage
{
	public ProductDetailsPage(ProductDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}