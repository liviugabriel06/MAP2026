using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;
using TaskManager.Core.Notifications;
using TaskManager.Core.Services;
using TaskManager.Data;
using TaskManager.UI;

namespace TaskManager.UI;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var services = new ServiceCollection();

        services.AddSingleton<ITaskRepository>(provider => new SqliteTaskRepository("tasks.db"));

        services.AddSingleton<ITaskReader>(provider => provider.GetRequiredService<ITaskRepository>());

        services.AddTransient<TaskValidator>();
        services.AddTransient<TaskService>();
        services.AddTransient<ReportService>();

        services.AddTransient<MainForm>();

        services.AddTransient<IReadOnlyDictionary<NotificationType, ITaskNotifier>>(provider =>
        {
            var notifiers = new Dictionary<NotificationType, ITaskNotifier>
            {
                {NotificationType.Console, new ConsoleNotifier()},
                {NotificationType.Email, new EmailNotifier()},
                {NotificationType.FileLog, new FileLogNotifier()}
            };
            return notifiers;
        });

        var serviceProvider = services.BuildServiceProvider();

        Application.Run(serviceProvider.GetRequiredService<MainForm>());
    }
}