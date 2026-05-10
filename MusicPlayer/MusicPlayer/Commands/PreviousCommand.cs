using MusicPlayer.Controllers;

namespace MusicPlayer.Commands;

public class PreviousCommand : IPlayerCommand
{
    private readonly PlaybackController _controller;

    public PreviousCommand(PlaybackController controller) => _controller = controller;

    public bool CanUndo => false;
    public string Description => "Previous Track";

    public void Execute() => _controller.PlayPrevious();
    public void Undo() { }
}