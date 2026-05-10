using MusicPlayer.Audio;

namespace MusicPlayer.Commands;

public class PlayCommand : IPlayerCommand
{
    private readonly AudioPlayer _player;

    public PlayCommand(AudioPlayer player) => _player = player;

    public bool CanUndo => false;
    public string Description => "Play";

    public void Execute() => _player.Play();
    public void Undo() { }
}