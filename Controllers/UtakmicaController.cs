using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
namespace Prvenstvo.Controllers;
 public class UtakmicaController : ControllerBase
 {
    UtakmicaContext Context {get; set; } = null!;

    public UtakmicaController(UtakmicaContext context)
    {
        Context = context;
    }

    [HttpPost("DodajUtakmicu/{idTima}/{idStadiona}")]
    public async Task<ActionResult> DodajUtakmicu(int idTima, int idStadiona, [FromBody] Utakmica utakmica)
    {
        var tim = await Context.Timovi.FindAsync(idTima);
        var stadion = await Context.Stadioni.FindAsync(idStadiona);
        if(tim!= null && stadion != null && utakmica.BrojPosetioca < 0 && utakmica.Posecenost < 0)
        {
            utakmica.Tim = tim;
            utakmica.Stadion = stadion;
            await Context.Utakmice.AddAsync(utakmica);
            await Context.SaveChangesAsync();
            return Ok("Uspešno upisana utakmica");
        }
        else{
            return BadRequest("Podaci su nevalidni");
        }
    }

    [HttpDelete("IzbrisiUtakmicu/{id}")]
    public async Task<ActionResult> IzbrisiUtakmicu(int id)
    {
        try
        {
            var utakmica = await Context.Utakmice.FindAsync(id);
            if(utakmica != null)
            {
                Context.Utakmice.Remove(utakmica);
                await Context.SaveChangesAsync();
            }
            return Ok($"Obrisana je utakmica sa IDem {id}");

        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
 }