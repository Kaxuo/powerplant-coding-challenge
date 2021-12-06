namespace powerplant_coding_challenge.Models
{
    public class Powerplants
    {
        public string name { get; set; }
        public string type { get; set; }
        public decimal efficiency { get; set; }
        public int pmin { get; set; }
        public int pmax { get; set; }
        public int power { get; set; } = 0;
    }
}