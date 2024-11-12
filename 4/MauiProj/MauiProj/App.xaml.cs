using MauiProj.Views;

namespace MauiProj
{
    public partial class App : Application
    {
        
        public App(ProductsPage productsPage)
        {
            InitializeComponent();
            MainPage = new NavigationPage(productsPage);
        }
    }
}
