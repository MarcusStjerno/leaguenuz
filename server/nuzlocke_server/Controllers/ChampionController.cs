using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json; // Add this namespace for ReadFromJsonAsync extension

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("champs")]
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

                var champions = championsData.Data.Values.Select(championDetails => new Champion
                {
                    Id = championDetails.Id,
                    key = championDetails.Key,
                    Name = championDetails.Name,
                    Title = championDetails.Title,
                    Blurb = championDetails.Blurb,
                    ImageUrl = $"https://ddragon.leagueoflegends.com/cdn/11.15.1/img/champion/{championDetails.Image.Full}" // Adjust the URL as per Riot Games API structure
                }).ToList();

                HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
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
        public string Key { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public ChampionImage Image { get; set; } // Nested class for image details
    }


    public class Champion
    {
        public string Id { get; set; }
        public string key { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Blurb { get; set; }
        public string ImageUrl { get; set; } // Property for storing image URL
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
