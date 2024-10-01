using Microsoft.Extensions.Caching.Memory;
using CommonLibrary.BusinessEntities;
using Common.BusinessEntities;
using BLLayer.HelperMethods;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using DALayer;
using System.Web;
using Common.Helper_Methods;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace BLLayer
{
    public class BusinessLogicLayer
    {
        private readonly DataAccessLayer _dataAccessLayer;

        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _config;

        public BusinessLogicLayer(ApplicationDBContext dbContext, IMemoryCache memoryCache, 
            IConfiguration config)
        {
            _dataAccessLayer = new DataAccessLayer(dbContext);
            _memoryCache = memoryCache;
            _config = config;
        }

        public List<VideosDto>? GetHomePageVideos()
        {
            const string cacheKey = "HomePageVideos";

            if (_memoryCache.TryGetValue(cacheKey, out List<VideosDto> cachedVideos))
            {
                return cachedVideos;
            }

            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }

            List<Video> homePageVideos = _dataAccessLayer.GetHomePageVideos();

            if (homePageVideos == null)
                return null;

            DtoConvertor videoDtoConvertor = new DtoConvertor();
            var videos = videoDtoConvertor.ToVideosDto(homePageVideos);

            _memoryCache.Set(cacheKey, videos, TimeSpan.FromMinutes(10));

            return videos;
        }

        private void ClearCache()
        {
            const string cacheKey = "HomePageVideos";
            _memoryCache.Remove(cacheKey);
        }


        public VideosDto? GetVideo(string id, string userId)
        {
            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }

            Video? video = _dataAccessLayer.GetVideo(id);

            if (video == null)
                return null;

            return DtoConvertor.ToVideosDto(video);
        }

        public bool DeleteVideo(string id)
        {
            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }


            bool isDeleted = _dataAccessLayer.DeleteVideo(id);

            if (isDeleted)
            {
                ClearCache();
            }

            return isDeleted;
        }

        public bool PostVideo(AddVideoDto video, int userId)
        {
            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }

            VideoDtoToVideo videoDtoToVideo = new VideoDtoToVideo();
            var DtoTovideo = videoDtoToVideo.DtoToVideo(video, userId);

            bool isPosted = _dataAccessLayer.PostVideo(DtoTovideo);

            if (isPosted)
            {
                ClearCache();
            }

            return isPosted;
        }

        public VideosDto? PostVideoVote(VideoUserAction video)
        {
            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }

            ClearCache();
            return DtoConvertor.ToVideosDto( _dataAccessLayer.PostVideoVote(video));
        }

        public VideosDto PostVideoBookmark(VideoUserAction video)
        {
            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }

            ClearCache();
            return DtoConvertor.ToVideosDto(_dataAccessLayer.PostVideoBookmark(video));
        }

        public VideosDto EditVideoBookmark(VideoUserAction video)
        {
            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }

            ClearCache();
            return DtoConvertor.ToVideosDto(_dataAccessLayer.EditVideoBookmark(video));
        }
        public UserDTO PostUser(CreateUser userDto)
        {
            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }

            //Encrypt Password
            userDto.Password = new PasswordHasher().HashPassword(userDto.Password);

            //Convert UserDTO to User
            var user = DtoConvertor.ToUser(userDto);

            var newUser = _dataAccessLayer.PostUser(user);

            return new DtoConvertor().ToUserDto(newUser);
        }

        public UserDTO GetUser(string email)
        {
            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }

            var user = _dataAccessLayer.GetUser(email);

            if (user == null)
                return null;

            DtoConvertor userDtoConvertor = new DtoConvertor();
            return userDtoConvertor.ToUserDto(user);

        }

        public UserDTO GetUser(int id)
        {
            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }

            var user = _dataAccessLayer.GetUser(id);

            if (user == null)
                return null;

            DtoConvertor userDtoConvertor = new DtoConvertor();
            return userDtoConvertor.ToUserDto(user);

        }

        public  User? AuthenticateUser(UserLogin userLogin) =>
             _dataAccessLayer.AuthenticateUser(userLogin);

        public string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Hash, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddYears(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public bool CheckExisitingUsers(string email) => _dataAccessLayer.CheckExisitingUsers(email);

        public UserDTO? DeleteUser(int id)
        {
            if (_dataAccessLayer == null)
            {
                throw new InvalidOperationException("DataAccessLayer is not initialized.");
            }

            var user = _dataAccessLayer.DeleteUser(id);

            if (user == null)
                return null;

            DtoConvertor userDtoConvertor = new DtoConvertor();
            return userDtoConvertor.ToUserDto(user);

        }

        public List<UserDTO> GetAllUsers()
        {
            var usersList = _dataAccessLayer.GetAllUsers();

            DtoConvertor userDtoConvertor = new DtoConvertor();
            return userDtoConvertor.ToUsersDto(usersList);
        }

        public UserDTO EditUser(UserDTOTemp user, int id)
        {
            var userEdited = _dataAccessLayer.EditUser(user, id);
            DtoConvertor userDtoConvertor = new DtoConvertor();
            return userDtoConvertor.ToUserDto(userEdited);
        }

        public VideosDto? EditVideo(EditVideo video)
        {
            var videoEdited = _dataAccessLayer.EditVideo(video);
            ClearCache();
            return DtoConvertor.ToVideosDto(videoEdited);
        }

        public VideoUserAction DtoConverter(VideoUserActionDto video, int userId)
        {
            return new VideoUserAction()
            {
                VideoId = video.VideoId,
                UserId = userId,
                VoteType = video.VoteType,
                isBookMarked = video.isBookMarked
            };
        }

        public VideosDto? AddVideoVotesandBookmark(VideosDto video, string userId)
        {
            return _dataAccessLayer.AddVideoVotesandBookmark(video, userId);
        }

        public object GetApplicationStats()
        {
            return _dataAccessLayer.GetApplicationStats();
        }
    }
}
