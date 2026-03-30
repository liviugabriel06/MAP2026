using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using TaskManager.Core.Models;
using TaskManager.Core.Services;
using TaskStatus = TaskManager.Core.Models.TaskStatus;

namespace TaskManager.UI;

public class MainForm : Form
{
    private readonly TaskService _taskService;
    private DataGridView? _gridTasks;
    private Button? _btnAdd;
    private Button? _btnComplete;
    private Button? _btnDelete;

    public MainForm(TaskService taskService)
    {
        _taskService = taskService;
        InitializeUI();
        LoadTasks();
    }

    private void InitializeUI()
    {
        Text = "Task Manager";
        Size = new System.Drawing.Size(850, 450);
        StartPosition = FormStartPosition.CenterScreen;

        _gridTasks = new DataGridView
        {
            Dock = DockStyle.Top,
            Height = 300,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        };

        // De implementat BtnAdd_Click, BtnComplete_Click, BtnDelete_Click
        _btnAdd = new Button { Text = "Adauga Task", Left=10, Top=320, Width=150, Height=40 };
        _btnAdd.Click += BtnAdd_Click;

        _btnComplete = new Button { Text = "Task marcat ca finalizat.", Left=170, Top=320, Width=150, Height=40 };
        _btnComplete.Click += BtnComplete_Click;

        _btnDelete = new Button { Text = "Sterge Task", Left=330, Top=320, Width=120, Height=40 };
        _btnDelete.Click += BtnDelete_Click;

        Controls.Add(_gridTasks);
        Controls.Add(_btnAdd);
        Controls.Add(_btnComplete);
        Controls.Add(_btnDelete);
    }

    private void LoadTasks()
    {
        _gridTasks.DataSource = null;
        _gridTasks.DataSource = _taskService.GetAllTasks().ToList();
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        var task = new DeadlineTask
        {
            Title = "Task nou generat la ora: " + DateTime.Now.ToLongTimeString(),
            Description = "Creat din interfata grafica",
            Priority = TaskPriority.High,
            DueDate = DateTime.Now.AddDays(2),
            NotificationType = NotificationType.FileLog
        };

        _taskService.AddTask(task);
        LoadTasks();
    }
    private void BtnComplete_Click(object? sender, EventArgs e)
    {
        if (_gridTasks.CurrentRow?.DataBoundItem is TaskItem selectedTask)
        {
            try
            {
                _taskService.CompleteTask(selectedTask.Id);
                LoadTasks();
                MessageBox.Show("Task-ul a fost finalizat cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare de finalizare!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (_gridTasks.CurrentRow?.DataBoundItem is TaskItem selectedTask)
        {
            var confirm = MessageBox.Show($"Esti sigur ca vrei sa stergi task-ul '{selectedTask.Title} ?", "Confirmare stergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                _taskService.DeleteTask(selectedTask.Id);
                LoadTasks();
            }
        }
    }

    
}