using BLLayer;
using Common;
using Common.BusinessEntities;
using Common.Helper_Methods;
using DALayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace PWPProject.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("/[controller]")]
    //[Authorize]
    public class VideoController : ControllerBase
    {
        private readonly BusinessLogicLayer _businessLogicLayer;
        private readonly AppSettings _appSettings;

        /// <summary>
        /// Video controller deals all types of operations related to the video
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="memoryCache"></param>
        /// <param name="config"></param>
        public VideoController(ApplicationDBContext dbContext, IMemoryCache memoryCache,
            IConfiguration config, IOptions<AppSettings> appSettings)
        {
            _businessLogicLayer = new BusinessLogicLayer(dbContext, memoryCache, config);
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Retrieves a video by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the video.</param>
        /// <returns>
        /// Returns an HTTP response containing a JSON payload with information about the retrieved video.
        /// </returns>
        /// <remarks>
        /// This endpoint fetches a video specified by its unique identifier. It first checks if the business logic layer
        /// is initialized. If not, it throws an InvalidOperationException. It then retrieves the video from the 
        /// business logic layer. If the video is found, it returns a success response (HTTP status code 200) along with the 
        /// retrieved video.
        /// </remarks>
        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromQuery] string? userId)
        {
            try
            {
                EnsureBusinessLogicLayerInitialized();

                VideosDto? video = _businessLogicLayer.GetVideo(id, userId);

                //check if video is voted by user
                video = _businessLogicLayer.AddVideoVotesandBookmark(video, userId);

                if (_appSettings.UseURLConvertor)
                {
                    //URL Convertor
                    video.Url = video.GetUrlEncodedUrl();
                    video.Tags = string.IsNullOrEmpty(video.Tags) ? video.Tags : video.GetUrlEncodedTags();
                    video.Description = string.IsNullOrEmpty(video.Description) ? video.Description : video.GetUrlEncodedDescription();
                }


                return video != null ? Ok(CreateVideoResponse(video)) : NotFound(new GetResponse<object>
                {
                    StatusCode = 404,
                    Message = "Video not found",
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext?.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult ApproveVideo(EditVideo video)
        {
            try
            {
                if (_businessLogicLayer == null)
                    throw new InvalidOperationException("Business Logic Layer is not initialized.");

                VideosDto? editedVideo = _businessLogicLayer.EditVideo(video);

                if (_appSettings.UseURLConvertor)
                {

                    //URL Convertor if null or empty dont convert

                    editedVideo.Url =  editedVideo.GetUrlEncodedUrl();
                    editedVideo.Tags = string.IsNullOrEmpty(editedVideo.Tags) ? editedVideo.Tags : editedVideo.GetUrlEncodedTags();
                    editedVideo.Description = string.IsNullOrEmpty(editedVideo.Description) ? editedVideo.Description : editedVideo.GetUrlEncodedDescription();
                }



                return editedVideo != null ? Ok(
                    new GetResponse<VideosDto>
                    {
                        StatusCode = 200,
                        Message = "Video Updated Successfully",
                        Data = editedVideo,
                        Timestamp = DateTime.UtcNow,
                        RequestId = HttpContext.TraceIdentifier,
                        Controls = CreateControl.CreateControlDictionary()
                    }

                ) : NotFound(new GetResponse<object>
                {
                    StatusCode = 404,
                    Message = "User not found",
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext?.TraceIdentifier
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(string id)
        {
            try
            {
                EnsureBusinessLogicLayerInitialized();

                bool isDeleted = _businessLogicLayer.DeleteVideo(id);


                return isDeleted ? Ok(DeleteObjResponse($"{ApplicationConstants.VideoBaseUrl}/GetVideo/{id}")) : NotFound(new GetResponse<object>
                {
                    StatusCode = 404,
                    Message = "Video not found",
                    Timestamp = DateTime.UtcNow,
                    RequestId = HttpContext?.TraceIdentifier
                });

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
            return StatusCode(404, CreateErrorResponse(ex.Message));
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

        private GetResponse<object> DeleteObjResponse(string video)
        {
            return new GetResponse<object>
            {
                StatusCode = 200,
                Message = "Deleted Successfully",
                
                Timestamp = DateTime.UtcNow,
                RequestId = HttpContext.TraceIdentifier
            };
        }

        private GetResponse<object> CreatePostVideoResponse(string videoAccessUrl, string videoId)
        {
            return new GetResponse<object>
            {
                StatusCode = 201,
                Message = "Success",
                Data = videoAccessUrl,
                Timestamp = DateTime.UtcNow,
                RequestId = HttpContext.TraceIdentifier,
                Controls = CreateControl.CreateControlDictionary(videoId)
            };
        }

    }
}
