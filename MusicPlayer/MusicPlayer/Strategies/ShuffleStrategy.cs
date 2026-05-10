using System;
using System.Collections.Generic;
using System.Linq;
using MusicPlayer.Models;

namespace MusicPlayer.Strategies;

public class ShuffleStrategy : IPlaybackStrategy
{
    private List<Track> _shuffledTracks = new();
    private int _currentIndex = -1;
    private readonly Random _random = new();

    public string Name => "Shuffle";

    public Track GetNextTrack(Playlist playlist, Track currentTrack)
    {
        if (playlist.Tracks.Count == 0) return null;

        if (_shuffledTracks.Count != playlist.Tracks.Count)
        {
            Reset(playlist);
        }

        _currentIndex++;
        if (_currentIndex >= _shuffledTracks.Count)
        {
            Reset(playlist);
            _currentIndex = 0;
        }

        return _shuffledTracks[_currentIndex];
    }

    public Track GetPreviousTrack(Playlist playlist, Track currentTrack)
    {
        if (playlist.Tracks.Count == 0) return null;

        if (_shuffledTracks.Count != playlist.Tracks.Count)
        {
            Reset(playlist);
        }

        _currentIndex--;
        if (_currentIndex < 0)
        {
            _currentIndex = 0;
        }

        return _shuffledTracks[_currentIndex];
    }

    public void Reset(Playlist playlist)
    {
        _shuffledTracks = playlist.Tracks.OrderBy(x => _random.Next()).ToList();
        _currentIndex = -1;
    }
}