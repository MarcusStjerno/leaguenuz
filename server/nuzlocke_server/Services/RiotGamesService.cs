using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class RiotGamesService
{
    private readonly HttpClient _httpClient;

    public RiotGamesService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Champion>> GetChampionsAsync()
    {
        var response = await _httpClient.GetStringAsync("https://ddragon.leagueoflegends.com/cdn/11.15.1/data/en_US/champion.json");
        var championsData = JObject.Parse(response)["data"].Values<JObject>();

        return championsData.Select(champion => new Champion
        {
            Id = champion["id"].ToString(),
            Name = champion["name"].ToString(),
            Title = champion["title"].ToString(),
            Blurb = champion["blurb"].ToString(),
            Image = $"http://ddragon.leagueoflegends.com/cdn/11.15.1/img/champion/{champion["image"]["full"]}"
        });
    }
}
