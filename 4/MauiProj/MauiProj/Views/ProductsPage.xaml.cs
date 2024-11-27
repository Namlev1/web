using MauiProj.Models;
using Microsoft.Maui.Controls;
using MauiProj.Models;

namespace MauiProj.Views
{
    public partial class ProductsPage : ContentPage
    {
        public ProductsPage(ProductsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }
}
