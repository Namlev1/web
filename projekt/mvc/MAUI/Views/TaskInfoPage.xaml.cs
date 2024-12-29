using MAUI.ViewModels;

namespace MAUI.Views;

public partial class TaskInfoPage : ContentPage
{
	public TaskInfoPage(TaskInfoViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}