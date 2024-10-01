using BLLayer;
using Common.BusinessEntities;
using Common.Helper_Methods;
using DALayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;

namespace PWPProject.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    //[Authorize]
    public class VoteController : ControllerBase
    {
        private readonly BusinessLogicLayer _businessLogicLayer;

        /// <summary>
        /// Vote controller deals all types of operations related to the voting of videos
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="memoryCache"></param>
        /// <param name="config"></param>
        public VoteController(ApplicationDBContext dbContext, IMemoryCache memoryCache,
            IConfiguration config)
        {
            _businessLogicLayer = new BusinessLogicLayer(dbContext, memoryCache, config);
        }

        /// <summary>
        /// Posts a vote for a video.
        /// </summary>
        /// <param name="video">The video object along with user's action (like, dislike).</param>
        /// <returns>
        /// Returns an HTTP response indicating the success or failure of the operation.
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public IActionResult PostVideoVote(VideoUserActionDto video)
        {
            try
            {
                if (_businessLogicLayer == null)
                    throw new InvalidOperationException("Business Logic Layer is not initialized.");

                if (video.VoteType < 0 || video.VoteType > 5)
                    return BadRequest("Vote is not valid");

                int userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Hash)?.Value);

                var videoVote = _businessLogicLayer.DtoConverter(video, userId);

                VideosDto? votedVideo = _businessLogicLayer.PostVideoVote(videoVote);
                votedVideo.isVoted = true;

                return votedVideo != null ? Ok(
                    new GetResponse<VideosDto>
                    {
                        StatusCode = 200,
                        Message = "Success",
                        Data = votedVideo,
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
