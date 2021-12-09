using System;
using System.Collections.Generic;
using System.Linq;
using powerplant_coding_challenge.Models;
using powerplant_coding_challenge.Services;

namespace powerplant_coding_challenge.Methods
{
    public class PowerService : IPowerService
    {
        private static int GetTurbinePower(double power, double wind)
        {
            if (wind < 0)
            {
                wind = 0;
            }
            var multiplier = wind / 100;
            return Convert.ToInt32(power * multiplier);
        }
        public List<Powerplant> GetPowerUsage(List<Powerplant> powerplants, int load, int wind)
        {
            var powerProvided = 0;
            var overload = 0;
            foreach (var powerplant in powerplants)
            {
                if (powerplant.type == "windturbine")
                {

                    powerplant.power = GetTurbinePower(powerplant.pmax, wind);
                    powerProvided += powerplant.power;
                }
                else
                {
                    powerProvided += powerplant.pmax;
                    powerplant.power = powerplant.pmax;
                }
                if (powerProvided >= load)
                {
                    break;
                }
            }
            overload = powerProvided - load;
            if (overload > 0)
            {
                foreach (var powerplant in powerplants.Where(powerplant => powerplant.power != 0).Reverse())
                {
                    var tempValue = powerplant.power - overload;
                    if (tempValue < powerplant.pmin)
                    {
                        var difference = powerplant.power - powerplant.pmin;
                        overload -= difference;
                        powerplant.power = powerplant.pmin;
                    }
                    else
                    {
                        powerplant.power = tempValue;
                        overload = 0;
                        break;
                    }
                }
            }
            return powerplants;
        }
    }
}