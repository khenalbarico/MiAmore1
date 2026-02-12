using LogicLbrary1.Models.Music;
using System;
using System.Collections.Generic;
using System.Text;

namespace LogicLbrary1.MusicPlaylistHandler1;

public sealed class MusicPlayerState
{
    public IReadOnlyList<MusicBaseModel> Playlist { get; private set; } = Array.Empty<MusicBaseModel>();
    public int Index { get; private set; } = 0;

    public bool IsReady => Playlist.Count > 0;
    public MusicBaseModel? Current => IsReady ? Playlist[Index] : null;

    public event Action? OnChange;

    public void SetPlaylist(IReadOnlyList<MusicBaseModel> list)
    {
        Playlist = list ?? Array.Empty<MusicBaseModel>();
        Index = 0;
        OnChange?.Invoke();
    }

    public void Next()
    {
        if (!IsReady) return;
        Index = (Index + 1) % Playlist.Count;
        OnChange?.Invoke();
    }

    public void Prev()
    {
        if (!IsReady) return;
        Index = (Index - 1 + Playlist.Count) % Playlist.Count;
        OnChange?.Invoke();
    }

    public void SetIndex(int index)
    {
        if (!IsReady) return;
        if (index < 0 || index >= Playlist.Count) return;
        Index = index;
        OnChange?.Invoke();
    }
}
