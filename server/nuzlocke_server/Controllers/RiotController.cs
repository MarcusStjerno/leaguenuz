using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace SimpleApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class RiotController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
         private const string ApiKey = "RGAPI-6326c744-4ee9-4cc3-91bf-9676cbc3101d";
        public RiotController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

       [HttpGet("summoner/{name}/{tag}")]
        public async Task<IActionResult> GetSummonerInfo(string name, string tag)
        {
            try
            {
                // Using string interpolation to insert the name and tag into the request URL
                var requestUrl = $"https://europe.api.riotgames.com/riot/account/v1/accounts/by-riot-id/{name}/{tag}?api_key={ApiKey}";
                // Creating an HTTP client using the factory
                var httpClient = _httpClientFactory.CreateClient();

                // Sending the request and awaiting the response
                var response = await httpClient.GetAsync(requestUrl);

                // Checking if the response was successful
                if (response.IsSuccessStatusCode)
                {
                    // Reading the response content
                    var content = await response.Content.ReadAsStringAsync();
                    // Returning the content as JSON
                    HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
                    return Ok(content);
                }
                else
                {
                    // Returning an error message if the response was not successful
                    return StatusCode((int)response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Returning a generic error message in case of an exception
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}