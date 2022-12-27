namespace Models;
public class Utakmica
{
    public int ID { get; set; }
    public int Posecenost { get; set; }
    public int BrojPosetioca { get; set; }
    public Stadion? Stadion { get; set; }
    public Tim? Tim {get; set;}
    

}