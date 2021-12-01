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
                var test = payload;
                System.Console.WriteLine(test);
                return Ok(test);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
