using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/champs/")]
[ApiController]
public class ChampionsController : ControllerBase
{
    private readonly RiotGamesService _riotGamesService;

    public ChampionsController(RiotGamesService riotGamesService)
    {
        _riotGamesService = riotGamesService;
    }

    [HttpGet("champion")]
    public async Task<IEnumerable<Champion>> Get()
    {
        HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
        return await _riotGamesService.GetChampionsAsync();
    }
}