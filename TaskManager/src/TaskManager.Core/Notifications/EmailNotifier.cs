using System;
using MimeKit;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;

namespace TaskManager.Core.Notifications;

public class EmailNotifier : ITaskNotifier
{

    private readonly string _emailAddress = "Trece_Emailul_Aici@gmail.com";
    private readonly string _appPassword = "Trece_Parola_Aici";
    public void Notify(TaskItem task)
    {
        try
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Task Manager App", _emailAddress));
            message.To.Add(new MailboxAddress("Mine", _emailAddress));

            message.Subject = $"Task finalizat: {task.Title}";

            message.Body = new TextPart("plain")
            {
                Text = $"Salut \n Te informez ca task-ul '{task.Title}' a fost finalizat. \n Detalii: \n Descriere: {task.Description} \n Prioritate: {task.Priority} \n\n Zi frumoasa!"
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 465, true);

                client.Authenticate(_emailAddress, _appPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EmailNotifier] Eroare la trimiterea email-ului: {ex.Message}");
        }
    }
}