using LogicLbrary1.Models.Music;
using System.Net.Http.Json;

namespace LogicLbrary1.MusicPlaylistHandler1;

public sealed class MusicLibraryService
{
    private readonly HttpClient _http;
    private IReadOnlyList<MusicBaseModel>? _cache;

    public MusicLibraryService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<MusicBaseModel>> GetAllAsync()
    {
        if (_cache is not null) return _cache;

        var list = await _http.GetFromJsonAsync<List<MusicBaseModel>>("GetAllMusics.json")
                   ?? new List<MusicBaseModel>();

        _cache = list;
        return _cache;
    }
}
