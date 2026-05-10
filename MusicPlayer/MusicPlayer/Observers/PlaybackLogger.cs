using System;
using System.IO;
using MusicPlayer.Audio;
using MusicPlayer.Controllers;

namespace MusicPlayer.Observers;

public class PlaybackLogger
{
    private readonly string _logPath = "playback_log.txt";

    public PlaybackLogger(AudioPlayer player, PlaybackController controller)
    {
        player.TrackEnded += (s, e) => Log($"Track ended naturally: {player.CurrentTrack?.Title}");

        player.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(AudioPlayer.CurrentTrack) && player.CurrentTrack != null)
            {
                Log($"Track started: {player.CurrentTrack.Title} by {player.CurrentTrack.Artist}");
            }
        };

        controller.StrategyChanged += (s, strategyName) => Log($"Strategy changed to: {strategyName}");
    }

    private void Log(string message)
    {
        File.AppendAllText(_logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}");
    }
}