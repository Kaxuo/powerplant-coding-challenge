using System.Collections.Generic;

namespace powerplant_coding_challenge.Models
{
    public class Payload
    {
        public Payload()
        {
            powerplants = new List<Powerplants>();
        }
        public int load { get; set; }
        public Fuels fuels { get; set; }
        public List<Powerplants> powerplants { get; set; }
    }
}