using MAUI.ViewModels;

namespace MAUI.Views;

public partial class TaskListPage : ContentPage
{
	public TaskListPage(TaskListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}