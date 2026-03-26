using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;
using TaskManager.Core.Notifications;
using TaskManager.Core.Services;
using TaskManager.Data;

namespace TaskManager.UI;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var repository = new SqliteTaskRepository("tasks.db");

        var notifiers = new Dictionary<NotificationType, ITaskNotifier>
        {
            { NotificationType.Console, new ConsoleNotifier() },
            { NotificationType.Email, new EmailNotifier() },
            { NotificationType.FileLog, new FileLogNotifier() }
        };

        var validator = new TaskValidator();
        var taskService = new TaskService(repository, notifiers, validator);

        Application.Run(new MainForm(taskService));
    }
}