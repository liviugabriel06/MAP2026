using System;
using System.Collections.Generic;
using System.Linq;
using MusicPlayer.Models;

namespace MusicPlayer.Strategies;

public class SmartShuffleStrategy : IPlaybackStrategy
{
    private readonly Queue<Track> _history = new();
    private readonly Random _random = new();
    private const int MaxHistorySize = 5;

    public string Name => "Smart Shuffle";

    public Track GetNextTrack(Playlist playlist, Track currentTrack)
    {
        if (playlist.Tracks.Count == 0) return null;

        if (currentTrack != null && !_history.Contains(currentTrack))
        {
            _history.Enqueue(currentTrack);
        }

        int currentHistoryLimit = Math.Min(MaxHistorySize, Math.Max(0, playlist.Tracks.Count - 1));

        while (_history.Count > currentHistoryLimit)
        {
            _history.Dequeue();
        }

        var availableTracks = playlist.Tracks.Except(_history).ToList();

        if (availableTracks.Count == 0)
        {
            availableTracks = playlist.Tracks.ToList();
            _history.Clear();
        }

        var nextTrack = availableTracks[_random.Next(availableTracks.Count)];

        _history.Enqueue(nextTrack);
        while (_history.Count > currentHistoryLimit)
        {
            _history.Dequeue();
        }

        return nextTrack;
    }

    public Track GetPreviousTrack(Playlist playlist, Track currentTrack)
    {
        if (playlist.Tracks.Count == 0) return null;
        return playlist.Tracks.First();
    }

    public void Reset(Playlist playlist)
    {
        _history.Clear();
    }
}