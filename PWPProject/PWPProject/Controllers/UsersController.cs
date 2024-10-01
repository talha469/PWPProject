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
using DocumentFormat.OpenXml.Presentation;
using Microsoft.Extensions.Options;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;

namespace PWPProject.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly BusinessLogicLayer _businessLogicLayer;
        private readonly AppSettings _appSettings;

        public UsersController(ApplicationDBContext dbContext, IMemoryCache memoryCache, IConfiguration config, IOptions<AppSettings> appSettings)
        {
            _businessLogicLayer = new BusinessLogicLayer(dbContext, memoryCache, config);
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Create a New User.
        /// </summary>
        /// <returns>
        /// Returns an HTTP response containing a JSON payload with information about the new user.
        /// </returns>
        /// <remarks>
        ///This endpoint create a new user in the system. It first checks if the business logic layer
        /// is initialized. If not, it throws an InvalidOperationException. It then retrieves the user from the 
        /// business logic layer. If the user is created successfully, it returns a success response (HTTP status code 201) along with the 
        /// retrieved user. If the user in unable to create, it returns the error (HTTP status code 500)
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GetResponse<object>))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(GetResponse<object>))]

        public IActionResult User(CreateUser user)
        {
            try
            {
                if (_businessLogicLayer == null)
                {
                    throw new InvalidOperationException("Business Logic Layer is not initialized.");
                }

                if (_businessLogicLayer.CheckExisitingUsers(user.Email))
                {
                    return Conflict(new GetResponse<object>
                    {
                        StatusCode = 409,
                        Message = "User already exists",
                        Timestamp = DateTime.UtcNow,
                        RequestId = HttpContext.TraceIdentifier,
                        Controls = UsersControllersHelperResponses.GetControlsForUser()
                    });
                }

                var userDto = _businessLogicLayer.PostUser(user);

                if (_appSettings.UseURLConvertor)
                {
                    //URL Convertor
                    userDto.ImagePath = string.IsNullOrEmpty(userDto.ImagePath) ? userDto.ImagePath : userDto.GetUrlEncodedImagePath();
                    userDto.Username = userDto.GetUrlEncodedUserName();
                }


                var response = new GetResponse<object>
                {
                    StatusCode = 201,
                    Message = "Success",
                    Data = userDto,
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext.TraceIdentifier,
                    Controls = UsersControllersHelperResponses.GetControlsForUser()
                };

                // Constructing the URI to the newly created resource
                var locationUri = new Uri(ApplicationConstants.UserBaseUrl + $"{userDto.UserId}");

                return Created(locationUri, response);
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
            catch (Exception ex)
            {
                return StatusCode(500, new GetResponse<object>
                {
                    StatusCode = 500,
                    Message = "Internal Server Error",
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>
        /// Returns an HTTP response containing a JSON payload with information about the all users.
        /// </returns>
        /// <remarks>
        ///This endpoint return all users in the system. It first checks if the business logic layer
        /// is initialized. If not, it throws an InvalidOperationException. It then retrieves the user from the 
        /// business logic layer. If the user is created successfully, it returns a success response (HTTP status code 201) along with the 
        /// retrieved user. If the user in unable to create, it returns the error (HTTP status code 500)
        /// </remarks>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetResponse<object>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GetResponse<object>))]

        public IActionResult User()
        {
            try
            {
                if (_businessLogicLayer == null)
                {
                    throw new InvalidOperationException("Business Logic Layer is not initialized.");
                }

                var usersList = _businessLogicLayer.GetAllUsers();

                if (_appSettings.UseURLConvertor)
                {
                    foreach (var user in usersList)
                    {
                        user.ImagePath = string.IsNullOrEmpty(user.ImagePath) ? user.ImagePath : user.GetUrlEncodedImagePath();
                        user.Username = user.GetUrlEncodedUserName();
                    }
                }

                var response = new GetResponse<object>
                {
                    StatusCode = 200,
                    Message = "Success",
                    Data = usersList,
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext.TraceIdentifier,
                    Controls = UsersControllersHelperResponses.GetControlsForUser()
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new GetResponse<object>
                {
                    StatusCode = 404,
                    Message = "Not Found",
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext.TraceIdentifier,
                    Controls = UsersControllersHelperResponses.GetControlsForUser()
                });
            }
        }
    }
}
