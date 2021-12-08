using System;
using System.Collections.Generic;
using System.Linq;
using powerplant_coding_challenge.Models;

namespace powerplant_coding_challenge.Methods
{
    public class GetPower
    {
        public static int GetTurbinePower(double power, double wind)
        {
            var multiplier = wind / 100;
            return Convert.ToInt32(power * multiplier);
        }
        public static List<Powerplants> GetPowerUsage(List<Powerplants> powerplants, int load, int wind)
        {
            var powerProvided = 0;
            var overload = 0;
            foreach (var powerplant in powerplants)
            {
                if (powerplant.type == "windturbine")
                {
                    powerProvided += GetTurbinePower(powerplant.pmax, wind);
                    powerplant.power = GetTurbinePower(powerplant.pmax, wind);
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