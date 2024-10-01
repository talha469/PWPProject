using System.Diagnostics.Metrics;
using System.Net;
using System.Security.Claims;
using BLLayer;
using Common.BusinessEntities;
using Common.Helper_Methods;
using DALayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swagger.Net.Annotations;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;

namespace PWPProject.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("/[controller]")]

    public class BookmarkController : ControllerBase
    {
        private readonly BusinessLogicLayer _businessLogicLayer;

        /// <summary>
        /// Vote controller deals all types of operations related to the voting of videos
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="memoryCache"></param>
        /// <param name="config"></param>
        public BookmarkController(ApplicationDBContext dbContext, IMemoryCache memoryCache,
            IConfiguration config)
        {
            _businessLogicLayer = new BusinessLogicLayer(dbContext, memoryCache, config);
        }

        /// <summary>
        /// Posts a bookmark for a video.
        /// </summary>
        /// <param name="video">The video object along with user's action (like, dislike).</param>
        /// <returns>
        /// Returns an HTTP response indicating the success or failure of the operation.
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public IActionResult PostVideoBookmark(VideoUserActionDto video)
        {
            try
            {
                if (_businessLogicLayer == null)
                    throw new InvalidOperationException("Business Logic Layer is not initialized.");

                int userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Hash)?.Value);

                var bookmarkVideo = _businessLogicLayer.DtoConverter(video, userId);

                VideosDto? bookmarked = _businessLogicLayer.PostVideoBookmark(bookmarkVideo);

                return bookmarked != null ? Ok(
                    new GetResponse<VideosDto>
                    {
                        StatusCode = 200,
                        Message = "Success",
                        Data = bookmarked,
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
                return StatusCode(404, ex.Message);
            }
        }

        /// <summary>
        /// Edit a bookmark for a video.
        /// </summary>
        /// <param name="video">The video object along with user's action (like, dislike).</param>
        /// <remarks>
        ///     {
        ///         "VideoId": "some radnom guid",
        ///         "UserId": 1,
        ///         "bookMarkDate": "2024-04-16T12:00:00",
        ///         "isBookMarked": true
        ///     }
        /// </remarks>
        /// <returns>
        /// Returns an HTTP response indicating the success or failure of the operation with edited video bookmark.
        /// </returns>
        [HttpPut]
        [Authorize(Roles = "Admin, User")]

        public IActionResult EditVideoBookmark(VideoUserActionDto video)
        {
            try
            {
                if (_businessLogicLayer == null)
                    throw new InvalidOperationException("Business Logic Layer is not initialized.");


                int userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Hash)?.Value);


                var bookmarkedVideo = _businessLogicLayer.DtoConverter(video, userId);

                VideosDto? bookmarkVideo = _businessLogicLayer.EditVideoBookmark(bookmarkedVideo);


                return bookmarkVideo != null ? Ok(
                    new GetResponse<VideosDto>
                    {
                        StatusCode = 200,
                        Message = "Success",
                        Data = bookmarkVideo,
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
                return StatusCode(404, ex.Message);
            }
        }
    }
}
