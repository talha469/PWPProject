using Common;
using Common.BusinessEntities;
using CommonLibrary.BusinessEntities;

namespace BLLayer.HelperMethods
{
    internal class DtoConvertor
    {
        public static VideosDto ToVideosDto(Video video)
        {
            VideosDto videoDto = new VideosDto
            {
                Id = video.Id,
                VideoId = video.VideoId,
                Description = video.Description,
                Tags = video.Tags,
                Url = video.Url,
                VideoUploadedDate = video.VideoUploadedDate
            };

            
            //videoDto.Links = new Links()
            //{
            //    Self = new LinkInfo 
            //    {
            //        Href = $"{ApplicationConstants.VideoBaseUrl}/GetVideo/{video.VideoId}",
            //        Title = "Self",
            //        Method = "GET"
            //    }
            //};

            return videoDto;
        }

        public List<VideosDto> ToVideosDto(List<Video> videos)
        {
            List<VideosDto> videosDto = new List<VideosDto>();
            foreach (var video in videos)
            {
                videosDto.Add(ToVideosDto(video));
            }

            return videosDto;
        }

        public UserDTO ToUserDto(User user)
        {
            UserDTO userDto = new UserDTO();
            userDto.Email = user.Email;
            userDto.AccountCreatedDate = user.AccountCreatedDate;
            userDto.ImagePath = user.ImagePath;
            userDto.Username = user.Username;
            userDto.Role = user.Role;
            userDto.UserId = user.UserId;

            return userDto;
        }

        public List<UserDTO> ToUsersDto(List<User> users)
        {
            List<UserDTO> userssDto = new List<UserDTO>();
            foreach (var user in users)
            {
                userssDto.Add(ToUserDto(user));
            }

            return userssDto;
        }

        public static User ToUser(CreateUser userDto)
        {
            User user = new User
            {
                Email = userDto.Email,
                AccountCreatedDate = DateTime.Now,
                ImagePath = userDto.ImagePath,
                Username = userDto.Username,
                PasswordHash = userDto.Password,
                Role = "User"
                
            };

            return user;
        }

    }
}
