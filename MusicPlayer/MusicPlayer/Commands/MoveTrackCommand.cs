using MusicPlayer.Models;

namespace MusicPlayer.Commands;

public class MoveTrackCommand : IPlayerCommand
{
    private readonly Playlist _playlist;
    private readonly int _oldIndex;
    private readonly int _newIndex;

    public MoveTrackCommand(Playlist playlist, int oldIndex, int newIndex)
    {
        _playlist = playlist;
        _oldIndex = oldIndex;
        _newIndex = newIndex;
    }

    public bool CanUndo => true;
    public string Description => $"Move Track {_oldIndex + 1} to {_newIndex + 1}";

    public void Execute() => _playlist.Move(_oldIndex, _newIndex);
    public void Undo() => _playlist.Move(_newIndex, _oldIndex);
}