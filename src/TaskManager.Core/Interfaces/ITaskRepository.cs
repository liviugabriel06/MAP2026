using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.models;

namespace TaskManager.Core.Interfaces
{
    public interface ITaskRepository
    {
        IReadOnlyList<TaskItem> GetAll();
        TaskItem GetById(int id);
        void Add(TaskItem t);
        void Update(TaskItem t);
        void Delete(int id);
    }
}
