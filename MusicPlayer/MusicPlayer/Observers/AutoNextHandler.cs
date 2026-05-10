using MusicPlayer.Audio;
using MusicPlayer.Controllers;

namespace MusicPlayer.Observers;

public class AutoNextHandler
{
    public AutoNextHandler(AudioPlayer player, PlaybackController controller)
    {
        player.TrackEnded += (s, e) =>
        {
            controller.PlayNext();
        };
    }
}