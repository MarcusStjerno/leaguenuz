using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

// [ApiController]
// [Route("api")]
// public class SummonerController : ControllerBase
// {
//     private readonly IHttpClientFactory _httpClientFactory;
//     private readonly IConfiguration _configuration;

//     public SummonerController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
//     {
//         _httpClientFactory = httpClientFactory;
//         _configuration = configuration;
//     }

//     [HttpGet("summoner")]
//     public async Task<IActionResult> GetSummonerInfo(string name, string tagline)
//     {
//         try
//         {
//             var riotApiKey = "RGAPI-cb139c63-5909-4352-834c-f6e6cc84db03";
//             var httpClient = _httpClientFactory.CreateClient();

//             // Construct the request URL
//             var requestUrl = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/lulufizz/euw?api_key=RGAPI-cb139c63-5909-4352-834c-f6e6cc84db03";

//             // Create the request and add the API key to the headers
//             var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
           

//             // Send the request
//             var response = await httpClient.SendAsync(request);

//             // Check if the response is successful
//             if (!response.IsSuccessStatusCode)
//             {
//                 return BadRequest($"Error fetching summoner info: {response.ReasonPhrase}");
//             }

//             // Read the content and deserialize JSON response
//             var content = await response.Content.ReadAsStringAsync();
//             var summonerInfo = JsonConvert.DeserializeObject(content);
//             return Ok(summonerInfo);
//         }
//         catch (Exception ex)
//         {
//             return StatusCode(500, $"Internal server error: {ex.Message}");
//         }
//     }
// }

