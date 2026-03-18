using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.models;

namespace TaskManager.Core.Services
{ 
    public class TaskValidator
    {
        public void Validate(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
                throw new ArgumentException("Este necesar un titlu.");
            if (task.Title.Length > 200)
                throw new ArgumentException("Titlul nu poate avea mai mult de 200 de caractere.");
            if (task is DeadlineTask deadlineTask)
                if (deadlineTask.DueDate < DateTime.Now)
                    throw new ArgumentException("Data limita trebuie sa fie in viitor.");
        }
    }
}
