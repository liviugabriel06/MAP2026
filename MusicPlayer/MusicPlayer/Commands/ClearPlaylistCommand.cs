using System.Collections.Generic;
using System.Linq;
using MusicPlayer.Models;

namespace MusicPlayer.Commands;

public class ClearPlaylistCommand : IPlayerCommand
{
    private readonly Playlist _playlist;
    private List<Track> _savedTracks;

    public ClearPlaylistCommand(Playlist playlist)
    {
        _playlist = playlist;
    }

    public bool CanUndo => true;
    public string Description => "Clear Playlist";

    public void Execute()
    {
        _savedTracks = _playlist.Tracks.ToList();
        _playlist.Clear();
    }

    public void Undo()
    {
        if (_savedTracks == null) return;
        foreach (var track in _savedTracks)
        {
            _playlist.Add(track);
        }
    }
}