using MauiProj.Models;
using Microsoft.Maui.Controls;
using MauiProj.Models;

namespace MauiProj.Views;

public partial class ProductOverviewPage : ContentPage
{
	public ProductOverviewPage(ProductOverviewViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}