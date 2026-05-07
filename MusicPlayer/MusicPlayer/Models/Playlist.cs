using System.Collections.ObjectModel;

namespace MusicPlayer.Models;

public class Playlist
{
    public ObservableCollection<Track> Tracks { get; } = new();

    public void Add(Track track) => Tracks.Add(track);

    public void Remove(Track track) => Tracks.Remove(track);

    public void Insert(int index, Track track) => Tracks.Insert(index, track);

    public void Move(int oldIndex, int newIndex) => Tracks.Move(oldIndex, newIndex);

    public void Clear() => Tracks.Clear();
}