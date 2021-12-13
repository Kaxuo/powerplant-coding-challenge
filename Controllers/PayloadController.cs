using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using powerplant_coding_challenge.Logging;
using powerplant_coding_challenge.Models;
using powerplant_coding_challenge.Services;

namespace powerplant_coding_challenge.Controllers
{
    [ApiController]
    [Route("productionplan")]
    public class ProductionPlan : ControllerBase
    {
        private readonly Error logger;
        private readonly IPowerService powerService;
        public ProductionPlan(Error logger, IPowerService powerService)
        {
            this.logger = logger;
            this.powerService = powerService;
        }
        [HttpPost]
        public ActionResult<List<Response>> Post(Payload payload)
        {
            try
            {
                var load = payload.load;
                var fuels = payload.fuels;
                var capacity = payload.powerplants.Select(powerplant => powerplant.pmax).ToArray();
                var powerplants = new List<Powerplant>();
                var gasUsage = payload.powerplants.Where(powerplant => powerplant.type == "gasfired");
                var keroseneUsage = payload.powerplants.Where(powerplant => powerplant.type == "turbojet");
                var windUsage = payload.powerplants.Where(powerplant => powerplant.type == "windturbine");
                if (fuels.Wind > 0)
                {
                    powerplants.AddRange(windUsage);
                    powerplants.AddRange(gasUsage);
                    powerplants.AddRange(keroseneUsage);
                    powerService.GetPowerUsage(powerplants, load, fuels.Wind);
                }
                else
                {
                    powerplants.AddRange(gasUsage);
                    powerplants.AddRange(keroseneUsage);
                    powerService.GetPowerUsage(powerplants, load, fuels.Wind);
                    powerplants.AddRange(windUsage);
                }
                var response = powerplants.Select(powerplant => new Response(powerplant.name, powerplant.power));
                if (load > capacity.Sum())
                {
                    logger.Log("Load too high, not enough powerplants");
                }
                if (load < powerplants[0].pmin)
                {
                    logger.Log("Load too low, some power will be wasted");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.Log(ex.Message);
                return BadRequest(new { message = "Something bad happened" });
            }
        }
    }
}
