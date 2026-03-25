using System;
using System.IO;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Notifications;

public class FileLogNotifier : ITaskNotifier
{
    public void Notify(TaskItem task)
    {
        File.AppendAllText("tasks.log", $"[{DateTime.Now}] Sarcina finalizata: {task.Title} \n");
    }
}