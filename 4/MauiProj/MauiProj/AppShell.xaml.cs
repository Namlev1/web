using MauiProj.Views;

namespace MauiProj
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ProductOverviewPage), typeof(ProductOverviewPage));
            Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
        }
    }
}
