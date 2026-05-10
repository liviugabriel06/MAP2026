using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Win32;
using MusicPlayer.Audio;
using MusicPlayer.Commands;
using MusicPlayer.Controllers;
using MusicPlayer.Models;
using MusicPlayer.Observers;
using MusicPlayer.Strategies;

namespace MusicPlayer.ViewModels;
public class MainWindowViewModel : INotifyPropertyChanged
{
    private readonly PlaybackController _controller;
    private readonly CommandHistory _history;
    private readonly StatisticsTracker _statisticsTracker;
    private readonly PlaybackLogger _logger;
    private readonly AutoNextHandler _autoNext;

    public AudioPlayer Player { get; }
    public Playlist Playlist { get; }

    public StatisticsSnapshot Statistics => _statisticsTracker.Snapshot;
    public IEnumerable<IPlayerCommand> HistoryList => new List<IPlayerCommand>(_history.GetHistory());

    public ICommand PlayCmd { get; }
    public ICommand PauseCmd { get; }
    public ICommand NextCmd { get; }
    public ICommand PrevCmd { get; }
    public ICommand AddTrackCmd { get; }
    public ICommand UndoCmd { get; }
    public ICommand RedoCmd { get; }
    public ICommand SetSequentialCmd { get; }
    public ICommand SetShuffleCmd { get; }
    public ICommand SetSmartShuffleCmd { get; }
    public ICommand SetRepeatOneCmd { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public MainWindowViewModel()
    {
        Player = new AudioPlayer();
        Playlist = new Playlist();
        _history = new CommandHistory();

        var sequentialStrategy = new SequentialStrategy();
        _controller = new PlaybackController(Player, Playlist, sequentialStrategy);

        _statisticsTracker = new StatisticsTracker(Player);
        _logger = new PlaybackLogger(Player, _controller);
        _autoNext = new AutoNextHandler(Player, _controller);

        Player.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Player));
        _statisticsTracker.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Statistics));

        PlayCmd = new RelayCommand(_ =>
        {
            if (Player.CurrentTrack == null && Playlist.Tracks.Count > 0)
            {
                ExecuteCommand(new NextCommand(_controller));
            }
            else
            {
                ExecuteCommand(new PlayCommand(Player));
            }
        });
        PauseCmd = new RelayCommand(_ => ExecuteCommand(new PauseCommand(Player)));
        NextCmd = new RelayCommand(_ => ExecuteCommand(new NextCommand(_controller)));
        PrevCmd = new RelayCommand(_ => ExecuteCommand(new PreviousCommand(_controller)));

        UndoCmd = new RelayCommand(_ => { _history.Undo(); RefreshHistory(); }, _ => _history.CanUndo);
        RedoCmd = new RelayCommand(_ => { _history.Redo(); RefreshHistory(); }, _ => _history.CanRedo);

        AddTrackCmd = new RelayCommand(_ => AddTracks());

        SetSequentialCmd = new RelayCommand(_ => ChangeStrategy(new SequentialStrategy()));
        SetShuffleCmd = new RelayCommand(_ => ChangeStrategy(new ShuffleStrategy()));
        SetSmartShuffleCmd = new RelayCommand(_ => ChangeStrategy(new SmartShuffleStrategy()));
        SetRepeatOneCmd = new RelayCommand(_ => ChangeStrategy(new RepeatOneStrategy()));
    }

    private void ExecuteCommand(IPlayerCommand cmd)
    {
        _history.Execute(cmd);
        RefreshHistory();
    }

    private void RefreshHistory()
    {
        OnPropertyChanged(nameof(HistoryList));
        ((RelayCommand)UndoCmd).RaiseCanExecuteChanged();
        ((RelayCommand)RedoCmd).RaiseCanExecuteChanged();
    }

    private void ChangeStrategy(IPlaybackStrategy newStrategy)
    {
        ExecuteCommand(new ChangeStrategyCommand(_controller, newStrategy, new SequentialStrategy()));
    }

    private void AddTracks()
    {
        var dialog = new OpenFileDialog { Multiselect = true, Filter = "Audio Files|*.mp3;*.wav" };
        if (dialog.ShowDialog() == true)
        {
            foreach (var file in dialog.FileNames)
            {
                var duration = Mp3MetadataReader.GetDuration(file);
                var track = new Track(Guid.NewGuid().ToString(), System.IO.Path.GetFileNameWithoutExtension(file), "Unknown Artist", "Unknown Album", duration, file);
                ExecuteCommand(new AddTrackCommand(Playlist, track));
            }
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;

    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
    public void Execute(object parameter) => _execute(parameter);
    public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
}