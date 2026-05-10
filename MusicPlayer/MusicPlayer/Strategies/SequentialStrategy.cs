using System.Linq;
using MusicPlayer.Models;

namespace MusicPlayer.Strategies;

public class SequentialStrategy : IPlaybackStrategy
{
    public string Name => "Sequential";

    public Track GetNextTrack(Playlist playlist, Track currentTrack)
    {
        if (playlist.Tracks.Count == 0) return null;
        if (currentTrack == null) return playlist.Tracks.First();

        int index = playlist.Tracks.IndexOf(currentTrack);
        if (index == -1 || index == playlist.Tracks.Count - 1)
            return null;

        return playlist.Tracks[index + 1];
    }

    public Track GetPreviousTrack(Playlist playlist, Track currentTrack)
    {
        if (playlist.Tracks.Count == 0) return null;
        if (currentTrack == null) return playlist.Tracks.First();

        int index = playlist.Tracks.IndexOf(currentTrack);
        if (index <= 0)
            return playlist.Tracks.First();

        return playlist.Tracks[index - 1];
    }

    public void Reset(Playlist playlist) { }
}