using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms;
using TaskManager.Core.Models;
using TaskManager.Core.Services;
using TaskStatus = TaskManager.Core.Models.TaskStatus;

namespace TaskManager.UI;

public class MainForm : Form
{
    private readonly TaskService _taskService;
    private readonly ReportService _reportService;

    private TextBox? _txtTitle;
    private TextBox? _txtDescription;
    private ComboBox? _cmbTaskType;
    private ComboBox? _cmbPriority;
    private ComboBox? _cmbNotification;
    private DateTimePicker? _dtpDueDate;

    private DataGridView? _gridTasks;
    private Button? _btnAdd;
    private Button? _btnComplete;
    private Button? _btnDelete;

    public MainForm(TaskService taskService, ReportService reportService)
    {
        _taskService = taskService;
        _reportService = reportService;

        Text = "Task Manager - Versiunea 2";
        Size = new Size(1000,550);
        StartPosition = FormStartPosition.CenterScreen;

        SetupUI(); // de implementat
        LoadTasks(); // de implementat
    }
}