using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.Data.Sqlite;
using TaskManager.Core.Interfaces;
using TaskManager.Core.Models;
using TaskStatus = TaskManager.Core.Models.TaskStatus;

namespace TaskManager.Data;

public class SqliteTaskRepository : ITaskRepository
{
    private readonly string _connectionString;

    public SqliteTaskRepository(string dbPath = "tasks.db")
    {
        _connectionString = $"Data Source={dbPath}";
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE IF NOT EXISTS Tasks (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT NOT NULL,
                Description TEXT,
                Status TEXT NOT NULL,
                Priority INTEGER NOT NULL,
                TaskType TEXT NOT NULL,
                NotificationType TEXT NOT NULL,
                DueDate TEXT,
                RecurrenceInterval INTEGER,
                CreatedAt TEXT NOT NULL)";
        command.ExecuteNonQuery();
    }

    public IEnumerable<TaskItem> GetAll()
    {
        var tasks = new List<TaskItem>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Tasks";

        using var reader = command.ExecuteReader();
        while (reader.Read())
            tasks.Add(MapReaderToTask(reader));
        
        return tasks;
    }

    public TaskItem? GetById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Tasks WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
            return MapReaderToTask(reader);

        return null;
    }

    public void Add(TaskItem task)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            INSERT INTO Tasks (Title, Description, Status, Priority, TaskType, NotificationType, DueDate, RecurrenceInterval, CreatedAt)
            VALUES (@title, @description, @status, @priority, @taskType, @notificationType, @dueDate, @recurrenceInterval, @createdAt);
            SELECT last_insert_rowid();";

        AddParameters(command, task);

        task.Id = Convert.ToInt32(command.ExecuteScalar());
    }

    public void Update(TaskItem task)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE Tasks SET 
                Title = @title, Description = @desc, Status = @status, Priority = @priority, 
                TaskType = @type, NotificationType = @notif, DueDate = @dueDate, 
                RecurrenceInterval = @recInterval, CreatedAt = @createdAt
            WHERE Id = @id";

        command.Parameters.AddWithValue("@id", task.Id);
        AddParameters(command, task);

        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "DELETE FROM Tasks WHERE Id = @id";
        command.Parameters.AddWithValue("@id", id);

        command.ExecuteNonQuery();
    }

    private void AddParameters(SqliteCommand command, TaskItem task)
    {
        command.Parameters.AddWithValue("@title", task.Title);
        command.Parameters.AddWithValue("@desc", task.Description ?? string.Empty);
        command.Parameters.AddWithValue("@status", task.Status.ToString());
        command.Parameters.AddWithValue("@priority", (int)task.Priority);
        command.Parameters.AddWithValue("@type", task.Type.ToString());
        command.Parameters.AddWithValue("@notif", task.NotificationType.ToString());
        command.Parameters.AddWithValue("@createdAt", task.CreatedAt.ToString("O")); // ISO 8601

        command.Parameters.AddWithValue("@dueDate", task.DueDate.HasValue ? task.DueDate.Value.ToString("O") : DBNull.Value);
        command.Parameters.AddWithValue("@recInterval", task.RecurrenceInterval.HasValue ? task.RecurrenceInterval.Value : DBNull.Value);
    }

    private TaskItem MapReaderToTask(SqliteDataReader reader)
    {
        var taskType = Enum.Parse<TaskType>(reader.GetString(reader.GetOrdinal("TaskType")));

        TaskItem task = taskType switch
        {
            TaskType.Deadline => new DeadlineTask(),
            TaskType.Recurring => new RecurringTask(),
            _ => new TaskItem()
        };

        task.Id = reader.GetInt32(reader.GetOrdinal("Id"));
        task.Title = reader.GetString(reader.GetOrdinal("Title"));
        task.Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description"));
        task.Status = Enum.Parse<TaskStatus>(reader.GetString(reader.GetOrdinal("Status")));
        task.Priority = (TaskPriority)reader.GetInt32(reader.GetOrdinal("Priority"));
        task.NotificationType = Enum.Parse<NotificationType>(reader.GetString(reader.GetOrdinal("NotificationType")));
        task.CreatedAt = DateTime.Parse(reader.GetString(reader.GetOrdinal("CreatedAt")));

        if (!reader.IsDBNull(reader.GetOrdinal("DueDate")))
            task.DueDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("DueDate")));

        if (!reader.IsDBNull(reader.GetOrdinal("RecurrenceInterval")))
            task.RecurrenceInterval = reader.GetInt32(reader.GetOrdinal("RecurrenceInterval"));

        return task;
    }
}