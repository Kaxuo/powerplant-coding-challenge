using System.Collections.Generic;

namespace powerplant_coding_challenge.Models
{
    public class Payload
    {
        public Payload()
        {
            powerplants = new List<Powerplant>();
        }
        public int load { get; set; }
        public Fuels fuels { get; set; }
        public List<Powerplant> powerplants { get; set; }
    }
}