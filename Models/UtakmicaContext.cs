using Microsoft.EntityFrameworkCore;

namespace Models;
public class UtakmicaContext : DbContext 
{
    public DbSet<Tim> Timovi { get; set; } = null!;
    public DbSet<Stadion> Stadioni { get; set; } = null!;
    public DbSet<Utakmica> Utakmice { get; set; } = null!;

    public UtakmicaContext(DbContextOptions options) : base(options)
    {
        
    }
}