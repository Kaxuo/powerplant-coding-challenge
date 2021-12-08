using System;
using System.Collections.Generic;
using System.IO;
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
                var capacity = payload.powerplants.Select(powerplant => powerplant.pmax).ToArray();
                var powerplants = new List<Powerplants>();
                var gasUsage = payload.powerplants.Where(powerplant => powerplant.type == "gasfired");
                var keroseneUsage = payload.powerplants.Where(powerplant => powerplant.type == "turbojet");
                var windUsage = payload.powerplants.Where(powerplant => powerplant.type == "windturbine");
                if (fuels.Wind > 0)
                {
                    powerplants.AddRange(windUsage);
                    powerplants.AddRange(gasUsage);
                    powerplants.AddRange(keroseneUsage);
                    Methods.GetPower.GetPowerUsage(powerplants, load, fuels.Wind);
                }
                if (fuels.Wind == 0)
                {
                    powerplants.AddRange(gasUsage);
                    powerplants.AddRange(keroseneUsage);
                    Methods.GetPower.GetPowerUsage(powerplants, load, fuels.Wind);
                    powerplants.AddRange(windUsage);
                }
                var response = powerplants.Select(powerplant => new Response(powerplant.name, powerplant.power));
                if (load > capacity.Sum())
                {
                    var logger = new Logging.Error();
                    logger.Log("Load too high, not enough powerplants");
                }
                if (load < powerplants[0].pmin)
                {
                    var logger = new Logging.Error();
                    logger.Log("Load too low, some power will be wasted");
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                var logger = new Logging.Error();
                logger.Log(ex.Message);
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
