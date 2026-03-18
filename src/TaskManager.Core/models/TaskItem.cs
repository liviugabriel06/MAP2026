using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; protected set; }
        public Priority Priority { get; set; }
        public TaskType TaskType { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public void Complete()
        {
            if (Status == Status.Done)
                throw new InvalidOperationException("Task-ul este deja finalizat.");

            CompleteCore();

            if (Status != Status.Done)
                throw new InvalidOperationException("Eroare: Task-ul nu a fost marcat ca terminat.");
        }

        protected virtual void CompleteCore()
        {
            Status = Status.Done;
        }
    }
}
