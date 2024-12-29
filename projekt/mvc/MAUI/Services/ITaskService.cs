using MAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Services
{
    public interface ITaskService
    {
        Task<List<TaskModel>> GetAll();
        Task<TaskModel> GetById(int id);
        Task<TaskModel> Create(TaskModel task);
        Task<TaskModel> Update(TaskModel task);
        Task<bool> Delete(int id);

    }
 
}
