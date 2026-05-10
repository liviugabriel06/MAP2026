using MusicPlayer.Audio;

namespace MusicPlayer.Commands;

public class PauseCommand : IPlayerCommand
{
    private readonly AudioPlayer _player;

    public PauseCommand(AudioPlayer player) => _player = player;

    public bool CanUndo => false;
    public string Description => "Pause";

    public void Execute() => _player.Pause();
    public void Undo() { }
}