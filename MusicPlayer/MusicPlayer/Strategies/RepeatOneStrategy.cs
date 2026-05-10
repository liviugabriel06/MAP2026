using System.Linq;
using MusicPlayer.Models;

namespace MusicPlayer.Strategies;

public class RepeatOneStrategy : IPlaybackStrategy
{
    public string Name => "Repeat One";

    public Track GetNextTrack(Playlist playlist, Track currentTrack)
    {
        if (playlist.Tracks.Count == 0) return null;
        return currentTrack ?? playlist.Tracks.First();
    }

    public Track GetPreviousTrack(Playlist playlist, Track currentTrack)
    {
        if (playlist.Tracks.Count == 0) return null;
        return currentTrack ?? playlist.Tracks.First();
    }

    public void Reset(Playlist playlist) { }
}