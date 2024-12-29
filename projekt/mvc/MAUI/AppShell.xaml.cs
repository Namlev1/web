using MAUI.Views;
namespace MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(TaskInfoPage), typeof(TaskInfoPage));
            Routing.RegisterRoute(nameof(TaskCreatePage), typeof(TaskCreatePage));
        }
    }
}
