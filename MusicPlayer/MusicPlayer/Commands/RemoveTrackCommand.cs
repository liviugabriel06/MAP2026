using MusicPlayer.Models;

namespace MusicPlayer.Commands;

public class RemoveTrackCommand : IPlayerCommand
{
    private readonly Playlist _playlist;
    private readonly Track _track;
    private int _originalIndex;

    public RemoveTrackCommand(Playlist playlist, Track track)
    {
        _playlist = playlist;
        _track = track;
    }

    public bool CanUndo => true;
    public string Description => $"Remove \"{_track.Title}\"";

    public void Execute()
    {
        _originalIndex = _playlist.Tracks.IndexOf(_track);
        if (_originalIndex != -1)
        {
            _playlist.Remove(_track);
        }
    }

    public void Undo()
    {
        if (_originalIndex != -1)
        {
            _playlist.Insert(_originalIndex, _track);
        }
    }
}