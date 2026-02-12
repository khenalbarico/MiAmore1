using LogicLbrary1.Models.Music;
using System.Net.Http.Json;

namespace LogicLbrary1.MusicPlaylistHandler1;

public sealed class MusicLibraryService
{
    private readonly HttpClient _http;

    public MusicLibraryService(HttpClient http)
    {
        _http = http;
    }

    public async Task<IReadOnlyList<MusicBaseModel>> GetAllAsync(bool forceReload = false)
    {
        var list = await _http.GetFromJsonAsync<List<MusicBaseModel>>("GetAllMusics.json")
                   ?? new List<MusicBaseModel>();

        return list;
    }
}
