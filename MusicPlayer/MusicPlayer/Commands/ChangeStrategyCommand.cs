using MusicPlayer.Controllers;
using MusicPlayer.Strategies;

namespace MusicPlayer.Commands;

public class ChangeStrategyCommand : IPlayerCommand
{
    private readonly PlaybackController _controller;
    private readonly IPlaybackStrategy _newStrategy;
    private readonly IPlaybackStrategy _oldStrategy;

    public ChangeStrategyCommand(PlaybackController controller, IPlaybackStrategy newStrategy, IPlaybackStrategy oldStrategy)
    {
        _controller = controller;
        _newStrategy = newStrategy;
        _oldStrategy = oldStrategy;
    }

    public bool CanUndo => true;
    public string Description => $"Strategy: {_newStrategy.Name}";

    public void Execute() => _controller.SetStrategy(_newStrategy);
    public void Undo() => _controller.SetStrategy(_oldStrategy);
}