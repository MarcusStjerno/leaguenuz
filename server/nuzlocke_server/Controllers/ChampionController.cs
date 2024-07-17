using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json; // Add this namespace for ReadFromJsonAsync extension

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("chhamps")]
    public class ChampionsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChampionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync("https://ddragon.leagueoflegends.com/cdn/11.15.1/data/en_US/champion.json");

                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest($"Error fetching champions: {response.ReasonPhrase}");
                }

                var championsData = await response.Content.ReadFromJsonAsync<ChampionsResponse>();

                if (championsData == null || championsData.Data == null)
                {
                    return BadRequest("Invalid champions data received from Riot API.");
                }

                var champions = championsData.Data.Values.ToList();
                return Ok(champions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class ChampionsResponse
    {
        public Dictionary<string, ChampionDetails> Data { get; set; }
    }

    public class ChampionDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public ChampionImage Image { get; set; }
    }

    public class Champion
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public ChampionImage Image { get; set; } // Use a nested class for Image property
    }

    public class ChampionImage
    {
        public string Full { get; set; }
        public string Sprite { get; set; }
        public string Group { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
    }

}
