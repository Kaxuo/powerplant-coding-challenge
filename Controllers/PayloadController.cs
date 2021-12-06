using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using powerplant_coding_challenge.Models;

namespace powerplant_coding_challenge.Controllers
{
    [ApiController]
    [Route("productionplan")]
    public class ProductionPlan : ControllerBase
    {
        private readonly ILogger<ProductionPlan> _logger;

        public ProductionPlan(ILogger<ProductionPlan> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<Fuels> Post(Payload payload)
        {
            try
            {
                var load = payload.load;
                var fuels = payload.fuels;
                var powerplants = new List<Powerplants>();
                var gasUsage = payload.powerplants.Where(powerplant => powerplant.type == "gasfired");
                var keroseneUsage = payload.powerplants.Where(powerplant => powerplant.type == "turbojet");
                var windUsage = payload.powerplants.Where(powerplant => powerplant.type == "windturbine");
                if (fuels.Wind > 0)
                {
                    powerplants.AddRange(windUsage);
                    powerplants.AddRange(gasUsage);
                    powerplants.AddRange(keroseneUsage);
                    GetPowerUsage(powerplants, load);
                }
                if (fuels.Wind == 0)
                {
                    powerplants.AddRange(gasUsage);
                    powerplants.AddRange(keroseneUsage);
                    GetPowerUsage(powerplants, load);
                    powerplants.AddRange(windUsage);
                }
                return Ok(powerplants);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        public static double GetTurbinePower(double power, double wind)
        {
            var multiplier = wind / 100;
            return power * multiplier;
        }
        public static List<Powerplants> GetPowerUsage(List<Powerplants> powerplants, int load)
        {
            var powerProvided = 0;
            var overload = 0;
            foreach (var powerplant in powerplants)
            {
                powerProvided += powerplant.pmax;
                powerplant.power = powerplant.pmax;
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
