using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json; // Add this namespace

namespace Service
{
    public class RiotGamesService : IRiotGamesService
    {
        private readonly HttpClient _httpClient;

        public RiotGamesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Champion>> GetChampionsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://ddragon.leagueoflegends.com/cdn/11.15.1/data/en_US/champion.json");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Error fetching champions: {response.ReasonPhrase}");
                }

                var championsData = await response.Content.ReadFromJsonAsync<ChampionsResponse>();
                return new List<Champion>(championsData.Data.Values);
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                throw new Exception("Error fetching champions.", ex);
            }
        }
    }

    public class ChampionsResponse
    {
        public Dictionary<string, Champion> Data { get; set; }
    }

    public class Champion
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public string Image { get; set; }
    }
}
