using BLLayer;
using Common.BusinessEntities;
using Common.Helper_Methods;
using CommonLibrary.BusinessEntities;
using DALayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace PWPProject.Controllers
{

    [ApiController]
    [Route("/[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly BusinessLogicLayer _businessLogicLayer;

        public StatsController(ApplicationDBContext dbContext, IMemoryCache memoryCache, IConfiguration config)
        {
            _businessLogicLayer = new BusinessLogicLayer(dbContext, memoryCache, config);
        }


        [HttpGet]
        public IActionResult GET()
        {
            var stats = _businessLogicLayer.GetApplicationStats();

            if (stats != null)
            {
                return Ok(new GetResponse<ApplicationStats>
                {
                    StatusCode = 200,
                    Message = "Successfull",
                    Data = (ApplicationStats)stats,
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext?.TraceIdentifier,
                    Controls = UsersControllersHelperResponses.GetControlsForStats()
                });
            }

            return NotFound(new GetResponse<object>
            {
                StatusCode = 404,
                Message = "User not found",
                Timestamp = DateTime.UtcNow,
                RequestId = HttpContext?.TraceIdentifier,
                Controls = UsersControllersHelperResponses.GetControlsForStats()
            });
        }
    }
}


