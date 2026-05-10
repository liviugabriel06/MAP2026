using MusicPlayer.Controllers;

namespace MusicPlayer.Commands;

public class NextCommand : IPlayerCommand
{
    private readonly PlaybackController _controller;

    public NextCommand(PlaybackController controller) => _controller = controller;

    public bool CanUndo => false;
    public string Description => "Next Track";

    public void Execute() => _controller.PlayNext();
    public void Undo() { }
}