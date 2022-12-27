using System.ComponentModel.DataAnnotations;

namespace Models;
public class Stadion
{
    [Key]
    public int ID { get; set; }
    
    [MaxLength(50)]
    public string Naziv { get; set; } = null!;
    [MaxLength(50)]
    public string Lokacija { get; set; } = null!;
    [Range(10000,80000)]
    public int Kapacitet { get; set; }
    public DateTime Otvaranje { get; set; }
    public List<Utakmica>? OdigraneUtakmice {get; set; }

}