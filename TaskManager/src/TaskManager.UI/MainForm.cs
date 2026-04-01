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

        SetupUI();
        LoadTasks(); // de implementat
    }

    private void SetupUI()
    {
        var topPanel = new Panel { DockStyle.Top, Height = 90};
        
        topPanel.Controls.Add(new Label { Text="Titlu:", Location = new Point(10, 15), Width = 40});
        _txtTitle = new TextBox { Location = new Point(50, 12), Width = 150};
        topPanel.Controls.Add(_txtTitle);

        topPanel.Controls.Add(new Label {Text = "Descriere:", Location = new Point(210, 15), Width = 60});
        _txtDescription = new TextBox { Location = new Point(270, 12), Width = 150};
        topPanel.Controls.Add(_txtDescription);

        topPanel.Controls.Add(new Label {Text = "Tip:", Location = new Point(440, 15), Width = 30});
        _cmbTaskType = new ComboBox { Location = new Point(470, 12), Width = 100, DropDownStyle = ComboBoxStyle.DropDownList};
        _cmbTaskType.DataSource = Enum.GetValues(typeof(TaskType));
        topPanel.Controls.Add(_cmbTaskType);

        topPanel.Controls.Add(new Label { Text = "Prioritate:", Location = new Point(590, 15), Width = 60});
        _cmbPriority = new ComboBox { Location = new Point(650, 12), Width = 100, DropDownStyle = ComboBoxStyle.DropDownList};
        _cmbPriority.DataSource = Enum.GetValues(typeof(TaskPriority));
        topPanel.Controls.Add(_cmbPriority);

        topPanel.Controls.Add(new Label { Text = "Notificare: ", Location = new Point(10, 50), Width = 65});
        _cmbNotification = new ComboBox { Location = new Point(75, 47), Width = 125, DropDownStyle = ComboBoxStyle.DropDownList};
        _cmbNotification.DataSource = Enum.GetValues(typeof(NotificationType));
        topPanel.Controls.Add(_cmbNotification);

        topPanel.Controls.Add(new Label {Text = "Data limita: ", Location = new Point(210, 50), Width = 70});
        _dtpDueDate = new DateTimePicker { Location = new Point(280, 47), Width = 140, Format = DateTimePickerFormat.Short};
        topPanel.Controls.Add(_dtpDueDate);

        _btnAdd = new Button {Text = "Adauga Task", Location = new Point(470, 45), Width = 280, BackColor = Color.LightGreen, Font = new Font(Font, FontStyle.Bold)};
        _btnAdd.Click += BtnAdd_Click;
        topPanel.Controls.Add(_btnAdd);


        Controls.Add(topPanel);


        var bottomPanel = new FlowLayoutPanel {Dock = DockStyle.Bottom, Height = 50, FlowDirection = FlowDirection.LeftToRight, Padding = new Padding(10)};
        
        _btnComplete = new Button {Text = "Marcheaza Done", Width = 150};
        _btnComplete.Click += BtnComplete.Click;
        bottomPanel.Controls.Add(_btnComplete);

        _btnDelete = new Button {Text = "Sterge Task", Width = 120, ForeColor = Color.White, BackColor = Color.IndianRed};
        _btnDelete.Click += BtnDelete_Click;
        bottomPanel.Controls.Add(_btnDelete);


        _gridTasks = new DataGridView
        {
            Dock = DockStyle.Fill,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            ReadOnly = true,
            AllowUserToAddRows = false
        };
        Controls.Add(_gridTasks);
    }
}