using MAUI.ViewModels;

namespace MAUI.Views;

public partial class TaskCreatePage : ContentPage
{
	public TaskCreatePage(TaskCreateViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}