namespace Models;
public class Tim
{
    public int ID { get; set; }
    public string Ime { get; set; } = null!;
    public int Rezultat { get; set; }
    public string Faza { get; set; } = null!;
    public Stadion? Stadion { get; set; }
    public DateTime VremeIgranja { get; set; }
}