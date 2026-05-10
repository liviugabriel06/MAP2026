using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using NAudio.Wave;
using MusicPlayer.Models;

namespace MusicPlayer.Audio;

public class AudioPlayer : INotifyPropertyChanged, IDisposable
{
    private IWavePlayer _waveOut;
    private AudioFileReader _audioFileReader;
    private readonly DispatcherTimer _positionTimer;

    private Track _currentTrack;
    private PlayerState _state = PlayerState.Stopped;
    private float _volume = 0.5f;

    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler TrackEnded;

    public AudioPlayer()
    {
        _positionTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
        _positionTimer.Tick += (s, e) => OnPropertyChanged(nameof(Position));
    }

    public Track CurrentTrack
    {
        get => _currentTrack;
        private set { _currentTrack = value; OnPropertyChanged(); }
    }

    public PlayerState State
    {
        get => _state;
        private set { _state = value; OnPropertyChanged(); }
    }

    public TimeSpan Position
    {
        get => _audioFileReader?.CurrentTime ?? TimeSpan.Zero;
        set
        {
            if (_audioFileReader != null)
            {
                _audioFileReader.CurrentTime = value;
                OnPropertyChanged();
            }
        }
    }

    public TimeSpan Duration => _audioFileReader?.TotalTime ?? TimeSpan.Zero;

    public float Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            if (_waveOut != null) _waveOut.Volume = value;
            OnPropertyChanged();
        }
    }

    public void Load(Track track)
    {
        Stop();
        Cleanup();

        CurrentTrack = track;
        _audioFileReader = new AudioFileReader(track.FilePath);
        _waveOut = new WaveOutEvent { Volume = _volume };
        _waveOut.Init(_audioFileReader);
        _waveOut.PlaybackStopped += OnPlaybackStopped;

        OnPropertyChanged(nameof(Duration));
        OnPropertyChanged(nameof(Position));
    }

    public void Play()
    {
        if (_waveOut != null && State != PlayerState.Playing)
        {
            _waveOut.Play();
            State = PlayerState.Playing;
            _positionTimer.Start();
        }
    }

    public void Pause()
    {
        if (_waveOut != null && State == PlayerState.Playing)
        {
            _waveOut.Pause();
            State = PlayerState.Paused;
            _positionTimer.Stop();
        }
    }

    public void Stop()
    {
        if (_waveOut != null)
        {
            _waveOut.Stop();
            State = PlayerState.Stopped;
            _positionTimer.Stop();
            Position = TimeSpan.Zero;
        }
    }

    public void Seek(TimeSpan time)
    {
        Position = time;
    }

    private void OnPlaybackStopped(object sender, StoppedEventArgs e)
    {
        State = PlayerState.Stopped;
        _positionTimer.Stop();

        if (_audioFileReader != null && _audioFileReader.CurrentTime >= _audioFileReader.TotalTime)
        {
            TrackEnded?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Cleanup()
    {
        if (_waveOut != null)
        {
            _waveOut.PlaybackStopped -= OnPlaybackStopped;
            _waveOut.Dispose();
            _waveOut = null;
        }
        if (_audioFileReader != null)
        {
            _audioFileReader.Dispose();
            _audioFileReader = null;
        }
    }

    public void Dispose()
    {
        Cleanup();
        _positionTimer.Stop();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}