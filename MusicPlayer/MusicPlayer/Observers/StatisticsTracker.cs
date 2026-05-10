using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MusicPlayer.Audio;

namespace MusicPlayer.Observers;

public record StatisticsSnapshot(string TotalPlayed, string TopArtist, int Skips);

public class StatisticsTracker : INotifyPropertyChanged
{
    private TimeSpan _totalPlayed = TimeSpan.Zero;
    private int _skips = 0;
    private readonly Dictionary<string, int> _artistPlays = new();
    private Models.Track _currentTrack;

    public event PropertyChangedEventHandler PropertyChanged;

    public StatisticsSnapshot Snapshot => new(
        $"{(int)_totalPlayed.TotalHours}h {_totalPlayed.Minutes}m",
        _artistPlays.OrderByDescending(x => x.Value).Select(x => x.Key).FirstOrDefault() ?? "Unknown",
        _skips
    );

    public StatisticsTracker(AudioPlayer player)
    {
        player.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(AudioPlayer.CurrentTrack))
            {
                if (_currentTrack != null && player.Position.TotalSeconds > 0 && player.Position.TotalSeconds < 30)
                {
                    _skips++;
                }

                _currentTrack = player.CurrentTrack;

                if (_currentTrack != null)
                {
                    if (!_artistPlays.ContainsKey(_currentTrack.Artist))
                        _artistPlays[_currentTrack.Artist] = 0;
                    _artistPlays[_currentTrack.Artist]++;
                }

                OnPropertyChanged();
            }
            else if (e.PropertyName == nameof(AudioPlayer.Position) && player.State == Models.PlayerState.Playing)
            {
                _totalPlayed += TimeSpan.FromMilliseconds(200);
                if (_totalPlayed.Milliseconds == 0 && _totalPlayed.Seconds == 0)
                {
                    OnPropertyChanged();
                }
            }
        };
    }

    private void OnPropertyChanged()
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Snapshot)));
    }
}