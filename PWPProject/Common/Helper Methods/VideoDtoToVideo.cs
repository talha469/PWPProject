using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.BusinessEntities;
using static System.Net.WebRequestMethods;

namespace Common.Helper_Methods
{
    public class VideoDtoToVideo
    {
        public Video DtoToVideo(AddVideoDto video, int userId)
        {

            return new Video
            {
                VideoId = video.VideoId,
                Url = $"https://vz-6258ec42-3d0.b-cdn.net/{video.VideoId}/playlist.m3u8",
                VideoUploadedDate = DateTime.Now,
                Tags = video.Tags,
                Description = video.Description,
                UserId = userId,
                VideoExtension = "m3u8",
                IsEncoded = true,
                IsDismiss = false,
                IsApproved = true,
                IsSellingVideo = false
            };
        }
    }
}
