using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Interfaces;
using TaskManager.Core.models;

namespace TaskManager.Data
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>();
        private int _nextId = 1;

        public void Add(TaskItem t)
        {
            t.Id = _nextId++;
            _tasks.Add(t);
        }

        public void Delete(int id)
        {
            var task = GetById(id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }

        public IReadOnlyList<TaskItem> GetAll()
        {
            return _tasks.AsReadOnly();
        }

        public TaskItem GetById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public void Update(TaskItem t)
        {
            var existing = GetById(t.Id);
            if (existing == null)
            {
                throw new Exception("Task-ul nu exista!");
            }
        }
    }
}
