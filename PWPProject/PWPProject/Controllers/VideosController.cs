using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using Azure;
using BLLayer;
using Common;
using Common.BusinessEntities;
using DALayer;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using static System.Net.WebRequestMethods;
using Control = Common.BusinessEntities.Control;
using Video = Common.BusinessEntities.Video;
using Common.Helper_Methods;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace PWPProject.Controllers
{
    public class Class1
    {
    }

    [ApiController]
    [Route("/[controller]")]
    public class VideosController : ControllerBase
    {
        private readonly BusinessLogicLayer _businessLogicLayer;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Videos controller deals all types of operations related to the videos
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="memoryCache"></param>
        /// <param name="config"></param>
        public VideosController(ApplicationDBContext dbContext, IMemoryCache memoryCache,
            IConfiguration config, IOptions<AppSettings> appSettings)
        {
            _businessLogicLayer = new BusinessLogicLayer(dbContext, memoryCache, config);
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Retrieves a list of videos for the home page.
        /// </summary>
        /// <returns>
        /// Returns an HTTP response containing a JSON payload with information about the retrieved videos.
        /// </returns>
        /// <remarks>
        /// This endpoint fetches a list of videos to display on the home page. It first checks if the business logic layer
        /// is initialized. If not, it throws an InvalidOperationException. It then retrieves the home page videos from the 
        /// business logic layer. If videos are found, it returns a success response (HTTP status code 200) along with the 
        /// retrieved videos.
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetResponse<object>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GetResponse<object>))]
        public IActionResult Get()
        {
            try
            {
                EnsureBusinessLogicLayerInitialized();

                List<VideosDto>? videos = _businessLogicLayer.GetHomePageVideos();

                if(_appSettings.UseURLConvertor)
                {
                    foreach (var video in videos)
                    {
                        //if tags and description are empty dont send to url convertor
                        video.Tags = string.IsNullOrEmpty(video.Tags) ? video.Tags : video.GetUrlEncodedTags();
                        video.Description = string.IsNullOrEmpty(video.Description) ? video.Description : video.GetUrlEncodedDescription();
                        video.Url = video.GetUrlEncodedUrl();
                        
                    }
                }


                return Ok(CreateVideosResponse(videos ?? new List<VideosDto>()));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


        /// <summary>
        /// Posts a new video to the system.
        /// </summary>
        /// <param name="video">The video object to be posted.</param>
        /// <returns>
        /// Returns an HTTP response indicating the success or failure of the operation.
        /// </returns>
        /// <remarks>
        /// This endpoint creates a new video in the system. It first checks if the business logic layer
        /// is initialized. If not, it throws an InvalidOperationException.
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GetResponse<object>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GetResponse<object>))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(GetResponse<object>))]
        public IActionResult Post(AddVideoDto video)
        {
            try
            {
                EnsureBusinessLogicLayerInitialized();

                // Extracting user ID from JWT token
                int userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Hash)?.Value);


                bool isSuccessful = _businessLogicLayer.PostVideo(video, userId);

                if (isSuccessful)
                {
                    var videoAccessUrl = ApplicationConstants.VideoBaseUrl + $"{video.VideoId}";

                    if (_appSettings.UseURLConvertor)
                    {
                        videoAccessUrl = Common.Helper_Methods.URLConvertor.UrlConverter.ConvertToUrl(videoAccessUrl);
                    }
                    
                    

                    return Created(videoAccessUrl, CreatePostVideoResponse(videoAccessUrl, video.VideoId));
                }

                return StatusCode(409, CreateErrorResponse("Failed to Insert the data"));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

      
        private void EnsureBusinessLogicLayerInitialized()
        {
            if (_businessLogicLayer == null)
                throw new InvalidOperationException("Business Logic Layer is not initialized.");
        }

        private IActionResult HandleError(Exception ex)
        {
            return StatusCode(500, CreateErrorResponse(ex.Message));
        }

        private GetResponse<object> CreateErrorResponse(string message)
        {
            return new GetResponse<object>
            {
                StatusCode = 500,
                Message = message,
                Timestamp = DateTime.UtcNow,
                RequestId = HttpContext.TraceIdentifier
            };
        }

        private GetResponse<List<VideosDto>> CreateVideosResponse(List<VideosDto> videos)
        {
            return new GetResponse<List<VideosDto>>
            {
                StatusCode = 200,
                Message = "Success",
                Data = videos,
                Timestamp = DateTime.UtcNow,
                RequestId = HttpContext.TraceIdentifier,
                Controls = CreateControl.CreateControlDictionary()
            };
        }

        private GetResponse<VideosDto> CreateVideoResponse(VideosDto video)
        {
            return new GetResponse<VideosDto>
            {
                StatusCode = 200,
                Message = "Success",
                Data = video,
                Timestamp = DateTime.UtcNow,
                RequestId = HttpContext.TraceIdentifier,
                Controls = CreateControl.CreateControlDictionary()
            };
        }

        private GetResponse<object> CreateObjResponse(string video)
        {
            return new GetResponse<object>
            {
                StatusCode = 200,
                Message = "Success",
                Data = video,
                Timestamp = DateTime.UtcNow,
                RequestId = HttpContext.TraceIdentifier
            };
        }

        private GetResponse<object> CreatePostVideoResponse(string videoAccessUrl, string videoId)
        {
            return new GetResponse<object>
            {
                StatusCode = 201,
                Message = "Video Uploaded Successfully",
                Data = videoAccessUrl,
                Timestamp = DateTime.UtcNow,
                RequestId = HttpContext.TraceIdentifier,
                Controls = CreateControl.CreateControlDictionary(videoId)
            };
        }
    }
}
