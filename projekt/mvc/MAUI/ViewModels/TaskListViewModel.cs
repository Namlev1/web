using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Models;
using MAUI.Services;
using MAUI.Views;
using System.Collections.ObjectModel;

namespace MAUI.ViewModels;



public partial class TaskListViewModel : ObservableObject
{
    private readonly ITaskService _taskService;
    [ObservableProperty]
    private bool isConnection = true;

    [ObservableProperty]
    public ObservableCollection<TaskModel> _tasks;

    [ObservableProperty]
    private TaskModel _selectedTask;

    public TaskListViewModel(ITaskService taskService)
	{
        _taskService = taskService;
        GetTasks();
        
    }

    public async Task GetTasks()
    {
        var result = await _taskService.GetAll();
        if (result == null)
        {
            IsConnection = false;
            Tasks = [];
        }
        else
        {
            IsConnection = true;
            Tasks = new ObservableCollection<TaskModel>(result);
        }
        

        //Tasks = new ObservableCollection<TaskModel>() { new TaskModel {id=-1, name="test_task_1", done=false }, new TaskModel {id=-2, name="test_task_2", done=true } } ;
    }



    [RelayCommand]
    public async Task Details(TaskModel task)
    {
        SelectedTask = task;
        await Shell.Current.GoToAsync(nameof(TaskInfoPage), new Dictionary<string, object>
            {
                {"TaskModel",SelectedTask },
                {nameof(TaskListViewModel), this }
            });
    }

    [RelayCommand]
    public async Task Create()
    {
        await Shell.Current.GoToAsync(nameof(TaskCreatePage), new Dictionary<string, object>
            {
                {nameof(TaskListViewModel), this }
            });
    }




}