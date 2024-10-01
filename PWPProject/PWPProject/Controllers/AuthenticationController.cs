using BLLayer;
using Common.BusinessEntities;
using DALayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace PWPProject.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("/[controller]")]
    //[Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly BusinessLogicLayer _businessLogicLayer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="memoryCache"></param>
        /// <param name="config"></param>
        public AuthenticationController(ApplicationDBContext dbContext, IMemoryCache memoryCache, IConfiguration config)
        {
            _businessLogicLayer = new BusinessLogicLayer(dbContext, memoryCache, config);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            var user = _businessLogicLayer.AuthenticateUser(userLogin);

            if (user != null)
            {
                var token = _businessLogicLayer.Generate(user);
                return Ok(new { token = token });
            }

            return NotFound("User not found");
        }
    }
}
