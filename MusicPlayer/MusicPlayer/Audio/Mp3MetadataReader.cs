using System;
using NAudio.Wave;

namespace MusicPlayer.Audio;

public static class Mp3MetadataReader
{
    public static TimeSpan GetDuration(string filePath)
    {
        using var reader = new AudioFileReader(filePath);
        return reader.TotalTime;
    }
}