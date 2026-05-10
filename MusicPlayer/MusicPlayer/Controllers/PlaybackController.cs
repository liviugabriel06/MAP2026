using System;
using MusicPlayer.Audio;
using MusicPlayer.Models;
using MusicPlayer.Strategies;

namespace MusicPlayer.Controllers;

public class PlaybackController
{
    private readonly AudioPlayer _player;
    private readonly Playlist _playlist;
    private IPlaybackStrategy _strategy;

    public event EventHandler<string> StrategyChanged;

    public PlaybackController(AudioPlayer player, Playlist playlist, IPlaybackStrategy initialStrategy)
    {
        _player = player;
        _playlist = playlist;
        _strategy = initialStrategy;
    }

    public void SetStrategy(IPlaybackStrategy strategy)
    {
        _strategy = strategy;
        StrategyChanged?.Invoke(this, strategy.Name);
    }

    public void PlayNext()
    {
        var nextTrack = _strategy.GetNextTrack(_playlist, _player.CurrentTrack);
        if (nextTrack != null)
        {
            _player.Load(nextTrack);
            _player.Play();
        }
        else
        {
            _player.Stop();
        }
    }

    public void PlayPrevious()
    {
        var prevTrack = _strategy.GetPreviousTrack(_playlist, _player.CurrentTrack);
        if (prevTrack != null)
        {
            _player.Load(prevTrack);
            _player.Play();
        }
        else
        {
            _player.Stop();
        }
    }
}