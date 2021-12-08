using System.Collections.Generic;
using powerplant_coding_challenge.Models;

namespace powerplant_coding_challenge.Services
{
    public interface IPowerService
    {
        int GetTurbinePower(double power, double wind);
        List<Powerplant> GetPowerUsage(List<Powerplant> powerplants, int load, int wind);
    }
}