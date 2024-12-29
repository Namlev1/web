using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUI.Models;
using MAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.ViewModels
{
    [QueryProperty(nameof(TaskListViewModel), "TaskListViewModel")]
    public partial class TaskCreateViewModel : ObservableObject
    {
        private readonly ITaskService _taskService;

        public TaskCreateViewModel(ITaskService taskService)
        {
            _taskService = taskService;
            newTask = new TaskModel() { Name="", Done=false };
        }

        [ObservableProperty]
        private TaskModel newTask;

        private TaskListViewModel _taskListViewModel;
        public TaskListViewModel TaskListViewModel
        {
            get { return _taskListViewModel; }
            set { _taskListViewModel = value; }
        }

        [RelayCommand]
        public async Task Create()
        {
            var result = await _taskService.Create(newTask);
            if (result != null)
            {
                await TaskListViewModel.GetTasks();
                await Shell.Current.GoToAsync("../", true);
            }

        }

    }
}
