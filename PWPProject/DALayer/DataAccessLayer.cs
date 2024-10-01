using Common;
using Common.BusinessEntities;
using CommonLibrary.BusinessEntities;
using System.Data.Entity;

namespace DALayer
{
    public class DataAccessLayer
    {
        private readonly ApplicationDBContext _dbContext;

        public DataAccessLayer(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Video> GetHomePageVideos()
        {
            return _dbContext.Video.Where(p => p.IsDismiss == false)
                .Take(ApplicationConstants.HomePageVideosAmount).ToList();
        }

        public Video? GetVideo(string id)
        {
            return _dbContext.Video.FirstOrDefault(u => u.Id == id) ?? null;
        }

        public bool DeleteVideo(string id)
        {
            var videoToDelete = _dbContext.Video.FirstOrDefault(u => u.Id == id);

            try
            {
                if (videoToDelete != null)
                {
                    _dbContext.Video.Remove(videoToDelete);
                    _dbContext.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public bool PostVideo(Video video)
        {
            try
            {
                _dbContext.Video.Add(video);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public Video PostVideoVote(VideoUserAction video)
        {
            try
            {
                var existingVote =
                    _dbContext.Votes.FirstOrDefault(v => v.Id == video.VideoId && v.UserId == video.UserId);

                if (existingVote != null)
                {
                    existingVote.VoteType = video.VoteType;
                    existingVote.VoteDate = DateTime.Now;
                    _dbContext.SaveChanges();
                }

                else
                {

                    var vote = new Vote()
                    {
                        Id = video.VideoId, // Ensure this Id exists in the Video table
                        VoteType = video.VoteType,
                        UserId = video.UserId,
                        VoteDate = DateTime.Now,
                    };

                    _dbContext.Votes.Add(vote);
                    _dbContext.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var modifiedVideo = _dbContext.Video.Where(p => p.Id == video.VideoId).FirstOrDefault();

            return modifiedVideo;

        }

        public Video? PostVideoBookmark(VideoUserAction video)
        {
            try
            {
                var existingBookmark =
                    _dbContext.BookMark.FirstOrDefault(v => v.Id == video.VideoId && v.UserId == video.UserId);

                if (existingBookmark != null)
                {
                    existingBookmark.isBookMarked = video.isBookMarked;
                    existingBookmark.bookMarkDate = DateTime.Now;
                    existingBookmark.UserId = 
                    _dbContext.SaveChanges();
                }
                else
                {
                    var bookMark = new BookMark()
                    {
                        Id = video.VideoId,
                        bookMarkDate = DateTime.Now,
                        UserId = video.UserId,
                        isBookMarked = video.isBookMarked
                    };

                    _dbContext.BookMark.Add(bookMark);
                    _dbContext.SaveChanges();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            return _dbContext.Video.Where(p => p.Id == video.VideoId).FirstOrDefault();

        }

        public Video? EditVideoBookmark(VideoUserAction video)
        {
            try
            {
                var existingBookmark =
                    _dbContext.BookMark.FirstOrDefault(v => v.Id == video.VideoId && v.UserId == video.UserId);

                if (existingBookmark == null)
                {
                    return null;
                }

                existingBookmark.isBookMarked = video.isBookMarked;
                existingBookmark.bookMarkDate = DateTime.Now;
                _dbContext.SaveChanges();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            return _dbContext.Video.Where(p => p.VideoId == video.VideoId).FirstOrDefault(); ;

        }

        public User PostUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                var newUser = _dbContext.Users.Where(p => p.Email == user.Email).FirstOrDefault();

                return newUser;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public User? GetUser(string email)
        {
            if (_dbContext == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }

            User? user = _dbContext.Users.FirstOrDefault(p => p.Email == email);

            return user;
        }

        public User? GetUser(int id)
        {
            if (_dbContext == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }

            User? user = _dbContext.Users.FirstOrDefault(p => p.UserId == id);

            return user;
        }

        public User? AuthenticateUser(UserLogin userLogin)
        {
            var user = _dbContext.Users
                .FirstOrDefault(u => u.Email.ToLower() == userLogin.Email.ToLower());

            if (user == null)
                return null;

            var passwordMatched = new PasswordHasher().VerifyPassword(userLogin.PasswordHash, user.PasswordHash);

            if (passwordMatched)
            {
                return user;
            }

            return null;
        }

        public bool CheckExisitingUsers(string email) => _dbContext.Users.Any(u => u.Email == email);

        public List<User> GetUsers()
        {
            if (_dbContext == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }

            List<User> user = _dbContext.Users.ToList();

            return user;
        }

        public User DeleteUser(int id)
        {
            if (_dbContext == null)
            {
                throw new InvalidOperationException("Database context is not initialized.");
            }

            User? user = _dbContext.Users.FirstOrDefault(p => p.UserId == id);

            if (user == null)
                return null;

            _dbContext.Remove(user);
            _dbContext.SaveChangesAsync();

            return user;
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User EditUser(UserDTOTemp userDto, int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == id);

            if (user != null)
            {
                user.Username = userDto.Username;
                user.Email = userDto.Email;
                user.Role = userDto.Role;

                _dbContext.SaveChanges();

                // Return true indicating successful update
                return _dbContext.Users.FirstOrDefault(user => user.UserId == id);
            }
            else
            {
                // Handle case where user is not found
                throw new Exception("User not found.");
            }
        }


        public Video EditVideo(EditVideo video)
        {
            var videoItem = _dbContext.Video.FirstOrDefault(u => u.Id == video.Id);

            if (video != null)
            {
                videoItem.Tags = video.Tags;
                videoItem.Description = video.Description;

                _dbContext.SaveChanges();

                // Return true indicating successful update
                return _dbContext.Video.FirstOrDefault(video => video.VideoId == video.VideoId);
            }
            else
            {
                // Handle case where user is not found
                throw new Exception("Video not found.");
            }
        }

        public VideosDto? AddVideoVotesandBookmark(VideosDto video, string userId)
        {
            // Convert userId to integer outside of the LINQ expression
            int userIdInt = userId == null ? 0 : Convert.ToInt32(userId);

            // Check if voted and bookmarked
            var isVoted = _dbContext.Votes.Any(p => p.UserId == userIdInt && p.Id == video.Id);
            var isBookmarked = _dbContext.BookMark.Any(p => p.UserId == userIdInt && p.Id == video.Id && p.isBookMarked == true);

            // Assign the results to the video object
            video.isVoted = isVoted;
            video.isBookmarked = isBookmarked;

            return video;
        }

        public object GetApplicationStats()
        {
            return _dbContext.Stats.FirstOrDefault();
        }
    }
}
