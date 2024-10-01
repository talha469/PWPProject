using BLLayer;
using Common;
using Common.BusinessEntities;
using Common.Helper_Methods;
using CommonLibrary.BusinessEntities;
using DALayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Control = Common.BusinessEntities.Control;
using Microsoft.Extensions.Options;

namespace PWPProject.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly BusinessLogicLayer _businessLogicLayer;
        private readonly AppSettings _appSettings;

        public UserController(ApplicationDBContext dbContext, IMemoryCache memoryCache, IConfiguration config, IOptions<AppSettings> appSettings)
        {
            _businessLogicLayer = new BusinessLogicLayer(dbContext, memoryCache, config);
            _appSettings = appSettings.Value;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult User(int id)
        {
            try
            {
                if (_businessLogicLayer == null)
                {
                    throw new InvalidOperationException("Business Logic Layer is not initialized.");
                }

                UserDTO? user = _businessLogicLayer.GetUser(id);


                if (_appSettings.UseURLConvertor)
                {
                    //URL Convertor
                    user.ImagePath = string.IsNullOrEmpty(user.ImagePath) ? user.ImagePath : user.GetUrlEncodedImagePath();
                    user.Username = user.GetUrlEncodedUserName();
                }


                if (user == null)
                {
                    return NotFound(new GetResponse<object>
                    {
                        StatusCode = 404,
                        Message = "User not found",
                        Timestamp = DateTime.UtcNow,
                        RequestId = HttpContext?.TraceIdentifier,
                        Controls = UsersControllersHelperResponses.GetControlsForUser()
                    });
                }

                return Ok(new GetResponse<UserDTO>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = user,
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext?.TraceIdentifier,
                    Controls = UsersControllersHelperResponses.GetControlsForUser()
                }) ; 
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, new GetResponse<object>
                {
                    StatusCode = 500,
                    Message = ex.Message,
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext.TraceIdentifier
                });
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                if (_businessLogicLayer == null)
                {
                    throw new InvalidOperationException("Business Logic Layer is not initialized.");
                }

                UserDTO? user = _businessLogicLayer.DeleteUser(id);

                if (_appSettings.UseURLConvertor)
                {
                    //URL Convertor
                    user.ImagePath = string.IsNullOrEmpty(user.ImagePath) ? user.ImagePath : user.GetUrlEncodedImagePath();
                    user.Username = user.GetUrlEncodedUserName();
                }


                if (user == null)
                {
                    return NotFound(new GetResponse<object>
                    {
                        StatusCode = 404,
                        Message = "User not found",
                        Timestamp = DateTime.UtcNow,
                        RequestId = HttpContext?.TraceIdentifier,
                        Controls = UsersControllersHelperResponses.GetControlsForUser()
                    });
                }

                return Ok(new GetResponse<UserDTO>
                {
                    StatusCode = 200,
                    Message = "User Deleted Successfully",
                    Data = user,
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext?.TraceIdentifier,
                    Controls = UsersControllersHelperResponses.GetControlsForUser()
                });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(404, new GetResponse<object>
                {
                    StatusCode = 404,
                    Message = ex.Message,
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext.TraceIdentifier,
                    Controls = UsersControllersHelperResponses.GetControlsForUser()
                });
            }
            

        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(UserDTOTemp userDto)
        {
            try
            {
                if (_businessLogicLayer == null)
                {
                    throw new InvalidOperationException("Business Logic Layer is not initialized.");
                }

                UserDTO userEdited = _businessLogicLayer.EditUser(userDto, userDto.UserId);

                if (_appSettings.UseURLConvertor)
                {
                    //URL Convertor
                    userEdited.ImagePath = string.IsNullOrEmpty(userEdited.ImagePath) ? userEdited.ImagePath : userEdited.GetUrlEncodedImagePath();
                    userEdited.Username = userEdited.GetUrlEncodedUserName();
                }


                if (_appSettings.UseURLConvertor)
                {
                    //URL Convertor
                    userEdited.ImagePath = userEdited.GetUrlEncodedImagePath();
                    userEdited.Username = userEdited.GetUrlEncodedUserName();
                }

                return Ok(new GetResponse<UserDTO>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = userEdited,
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext?.TraceIdentifier,
                    Controls = UsersControllersHelperResponses.GetControlsForUser()
                });

            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(404, new GetResponse<object>
                {
                    StatusCode = 404,
                    Message = ex.Message,
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext.TraceIdentifier,
                    Controls = UsersControllersHelperResponses.GetControlsForUser()
                });
            }

            return null;
        }

        
    }
}
