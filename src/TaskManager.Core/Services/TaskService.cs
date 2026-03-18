using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Interfaces;
using TaskManager.Core.models;

namespace TaskManager.Core.Services
{
    public class TaskService
    {
        private readonly ITaskRepository _repository;
        private readonly TaskValidator _validator;
        private readonly IReadOnlyDictionary<NotificationType, ITaskNotifier> _notifiers;

        public TaskService(
            ITaskRepository repository,
            TaskValidator validator, 
            IReadOnlyDictionary<NotificationType, ITaskNotifier> notifiers)
        {
            _repository = repository;
            _validator = validator;
            _notifiers = notifiers;
        }

        public void AddTask(TaskItem task)
        {
            _validator.Validate(task);
            _repository.Add(task);
        }

        public void CompleteTask(int id)
        {
            var task = _repository.GetById(id);
            if (task != null)
                throw new Exception("Task-ul nu a fost gasit!");

            task.Complete();

            _repository.Update(task);

            if (_notifiers.TryGetValue(task.NotificationType, out var notifier))
                notifier.Notify(task);
        }
        public IReadOnlyList<TaskItem> GetAllTasks() => _repository.GetAll();
        public void DeleteTask(int id) => _repository.Delete(id);
    }
}
