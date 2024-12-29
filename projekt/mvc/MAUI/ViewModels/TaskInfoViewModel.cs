using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Models;
using MAUI.Services;
using System.Collections.ObjectModel;

namespace MAUI.ViewModels;

[QueryProperty("ViewedTask", "ViewedTask")]
[QueryProperty(nameof(TaskListViewModel), "TaskListViewModel")]
public partial class TaskInfoViewModel : ObservableObject
{
    private readonly ITaskService _taskService;

    public TaskInfoViewModel(ITaskService taskService)
	{
        _taskService = taskService;
    }

    [ObservableProperty]
    private TaskModel viewedTask;

    private TaskListViewModel _taskListViewModel;
    public TaskListViewModel TaskListViewModel
    {
        get { return _taskListViewModel; }
        set { _taskListViewModel = value; }
    }

    [RelayCommand]
    public async Task Save()
    {

        var result = await _taskService.Update(ViewedTask);
        if (result != null) {
            await TaskListViewModel.GetTasks();
            await Shell.Current.GoToAsync("../", true);
        }

    }

    [RelayCommand]
    public async Task Delete()
    {
        var result = await _taskService.Delete(ViewedTask.Id ?? default(int));
        if (result)
        {
            await TaskListViewModel.GetTasks();
            await Shell.Current.GoToAsync("../", true);
        }
    }


}