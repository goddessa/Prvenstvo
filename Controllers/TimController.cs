using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Prvenstvo.Controllers;
public class TimController : ControllerBase
{
    public UtakmicaContext Context {get; set; } = null!;
    public TimController(UtakmicaContext context)
    {
        Context = context;
    }
    [HttpPost("DodajTim")]
    public async Task<ActionResult> DodajTim([FromBody] Tim tim)
    {
        if(string.IsNullOrWhiteSpace(tim.Ime) || tim.Ime.Length > 50)
        {
            return Ok("Pogrešno ime tima!");
        }
        if(string.IsNullOrWhiteSpace(tim.Faza) || tim.Faza.Length > 50)
        {
            return Ok("Pogrešno ime faze");
        }
        if(tim.Rezultat < 0)
        {
            return Ok("Nevalidan rezultat");
        }
        try
        {
            await Context.Timovi.AddAsync(tim);
            await Context.SaveChangesAsync();
            return Ok(tim.ID);

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PreuzmiTimove")]
    public async Task<ActionResult> PreuzmiTimove()
    {
        var podaci = (await Context
        .Timovi
        .ToListAsync()).ToList();
        var grupisano = podaci
        .GroupBy(p=> p.Ime);
        return Ok(grupisano
        .ToDictionary(p=>p.Key, 
        q=> q.Select(r=>r.Stadion)));
    }

    [HttpPut("PromeniTim/{id}")]
    public async Task<ActionResult> PromeniTim(int id, [FromBody]Tim tim)
    {
        try
        {
            var timBaza = await Context.Timovi.FindAsync(id);
            if(timBaza != null)
            {
                timBaza.Ime = tim.Ime;
                timBaza.Rezultat = tim.Rezultat;
                timBaza.Faza = tim.Faza;
                Context.Timovi.Update(timBaza);
                await Context.SaveChangesAsync();
            }
            return Ok($"Uspešno promenjen tim sa IDem {id}");

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
