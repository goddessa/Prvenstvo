using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Prvenstvo.Controllers;
[ApiController]
[Route("[controller]")]
public class StadionController : ControllerBase
{
    public UtakmicaContext Context { get; set; } = null!;

    public StadionController(UtakmicaContext context)
    {
        Context = context;
    }
    [HttpPost("DodajStadion")]
    public async Task<ActionResult> DodajStadion([FromBody] Stadion stadion)
    {
        if(string.IsNullOrWhiteSpace(stadion.Naziv) || stadion.Naziv.Length > 50)
        {
            return BadRequest("Pogrešno ime");
        }
        if(string.IsNullOrWhiteSpace(stadion.Lokacija) || stadion.Lokacija.Length > 50)
        { 
            return BadRequest("Pogrešna lokacija!");
        }
        if(stadion.Kapacitet < 10000 || stadion.Kapacitet > 80000)
        {
            return BadRequest("Pogrešan kapacitet");
        }
        if(stadion.Otvaranje > DateTime.Now)
        {
            return BadRequest("Nije otvoren");
        }
        try
        {
            await Context.Stadioni.AddAsync(stadion);
            await Context.SaveChangesAsync();
            return Ok(stadion.ID);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);

        }
    }
    [HttpGet("PreuzmiStadione")]
    public async Task<ActionResult> PreuzmiStadione()
    {
        var stadioni = (await Context
        .Stadioni
        .ToListAsync()).ToList();
        var grupisano = stadioni
        .GroupBy(p=> p.Naziv);

        return Ok(grupisano
        .ToDictionary(p=> p.Key,
        q=> q.Select(r => r.Naziv)));
        
    }
}