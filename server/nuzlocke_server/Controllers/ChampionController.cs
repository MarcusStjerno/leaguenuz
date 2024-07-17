using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/champs")]
[ApiController]
public class ChampionsController : ControllerBase
{
    private readonly RiotGamesService _riotGamesService;

    public ChampionsController(RiotGamesService riotGamesService)
    {
        _riotGamesService = riotGamesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetChampions()
    {
        try
        {
            var champions = await _riotGamesService.GetChampionsAsync();
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
            return Ok(champions);
        }
        catch (Exception ex)
        {
            // Handle exceptions appropriately
            return StatusCode(500, "An error occurred while fetching champions.");
        }
    }
}
