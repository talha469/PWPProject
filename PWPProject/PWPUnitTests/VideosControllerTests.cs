using BLLayer;
using Common.BusinessEntities;
using Common.Enum;
using CommonLibrary.BusinessEntities;
using DALayer;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq.Protected;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Text;

namespace PWPUnitTests
{
    public class VideosControllerTests
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IConfiguration _config;
        private readonly UserLogin _userLogIn;
        private readonly User _user;

        private HttpClient _client;

        private string _token;
        public VideosControllerTests()
        {
            _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1L" +
                "zA1L2lkZW50aXR5L2NsYWltcy9oYXNoIjoiMSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpd" +
                "HkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoidGFsaGEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50a" +
                "XR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0YWxoYUBpYmV4LmNvIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS" +
                "9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoidGFsaGEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYva" +
                "WRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTgxMDUyMTU1MSwiaXNzIjoiaHR0cDovL05BLyIsImF1ZCI6Imh0dHA6L" +
                "y9OQS8ifQ.-1oFZxRc8OfWr9wGzeM-cdeydsjqpBdkDNsP0_h1gTQ";

            _dbContext = GetDatabase();

            // Create an instance of IConfiguration and populate it with test configuration values
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("Jwt:Issuer", "https://localhost:44381/"),
                    new KeyValuePair<string, string>("Jwt:Audience", "https://localhost:44381/"),
                    new KeyValuePair<string, string>("Jwt:Key", "DhftOS5uphK3vmCJQrexST1RsyjZBjXWRgJMFPU4")
                    // Add other configuration values as needed for your tests
                })
                .Build();


            UserLogin userLogin = new UserLogin()
            {
                Email = "talha469",
                PasswordHash = "talha469",
            };

            User user = new User()
            {
                Email = "talha469",
                PasswordHash = "talha469",
                Username = "talha469",
                Role = "User",
            };


            _user = user;
            _userLogIn = userLogin;
            _config = configuration;
        }

        public ApplicationDBContext GetDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database name for each test
                .Options;

            var dbContext = new ApplicationDBContext(options);

            // Seed some test data
            dbContext.Video.Add(new Video
            {
                VideoId = "uuid34",
                Tags = "BMW",
                Description = "Model 2024",
                Url = "https://newdeployed.api.com/video.mp4",
                IsApproved = false,
                AvailableResolutions = null,
                VideoUploadedDate = null, //VideoStatus = VideoStatus.IsIntroVideo,
            });

            dbContext.Users.Add(new User()
            {
                UserId = 1,
                Username = "talha469",
                Email = "talha469",
                ImagePath = "http://newImage.com/pic.jpg",
                Role = "User",
                PasswordHash = "LRSMdASJQliMDrPsaaj5QU3r7I7rHC1HrUz2jaq9nwaIz3EO7gJpDVpDy2hkv3o7"
            });

            dbContext.SaveChanges();

            return dbContext;
        }

        [Fact]
        public void GetJWTToken_ReturnDataAsIsValid_When_TokenGenerated_and_checked_Successfully()
        {
            // Act 
            var businessLogicLayer = new BusinessLogicLayer(_dbContext, new MemoryCache(new MemoryCacheOptions()), _config);
            var result = businessLogicLayer.Generate(_user);

            var handler = new JwtSecurityTokenHandler();

            // Attempt to parse the token string
            var isValidToken = handler.CanReadToken(result);

            ////Assert
            Assert.Equal(true, isValidToken);
        }

        [Fact]
        public void GetUser_ReturnIsAuntehticatedUser_When_UserFound()
        {
            // Arrange
            var controller = new DataAccessLayer(_dbContext);

            // Act 
            var result = controller.AuthenticateUser(_userLogIn);

            //Assert
            Assert.Equal("talha469", result.Email);
        }

        [Fact]
        public async Task GetUserbyId_ReturnsOk_WhenUserReturnedSuccessfully()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

            // Arrange

            var request = new HttpRequestMessage(HttpMethod.Get, "/user/1");
            request.Headers.Add("Authorization", "Bearer " + _token);

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetUserbyId_ReturnsNotFound_WhenUserNotFoundinDatabase()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

            // Arrange

            var request = new HttpRequestMessage(HttpMethod.Get, "/user/10000");
            request.Headers.Add("Authorization", "Bearer " + _token);

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetUserbyId_ReturnsBadRequest_WhenInputParamIsNotAcceptable()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

            // Arrange

            var request = new HttpRequestMessage(HttpMethod.Get, "/user/4kvideo");
            request.Headers.Add("Authorization", "Bearer " + _token);

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        //[Fact]
        //public async Task DeleteUserbyId_ReturnsBadRequest_WhenInputParamIsNotAcceptable()
        //{
        //    _client = new HttpClient();
        //    _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

        //    // Arrange

        //    var request = new HttpRequestMessage(HttpMethod.Delete, "/user/4kvideo");
        //    request.Headers.Add("Authorization", "Bearer " + _token);

        //    // Act
        //    var response = await _client.SendAsync(request);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        //}

        //[Fact]
        //public async Task DeleteUserbyId_ReturnsNotFound_WhenUserNotFoundinDatabase()
        //{
        //    _client = new HttpClient();
        //    _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

        //    // Arrange

        //    var request = new HttpRequestMessage(HttpMethod.Delete, "/user/10000");
        //    request.Headers.Add("Authorization", "Bearer " + _token);

        //    // Act
        //    var response = await _client.SendAsync(request);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        //}

        //[Fact]
        //public async Task DeleteUserbyId_ReturnsOk_WhenUserReturnedSuccessfully()
        //{
        //    _client = new HttpClient();
        //    _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

        //    // Arrange

        //    var request = new HttpRequestMessage(HttpMethod.Get, "/user/1");
        //    request.Headers.Add("Authorization", "Bearer " + _token);

        //    // Act
        //    var response = await _client.SendAsync(request);

        //    // Assert
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //}

        [Fact]
        public async Task PuttUser_ReturnsBadRequest_WhenInputParamIsNotAcceptable()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

            // Arrange
            var jsonBody = @"{

                        ""username"": ""theUser"",
                        ""email"": ""john@email.com"",
                        ""role"": ""Admin""
                    }";

            var request = new HttpRequestMessage(HttpMethod.Put, "/user");
            request.Headers.Add("Authorization", "Bearer " + _token);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutUser_ReturnsNotFound_WhenUserNotFoundinDatabase()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

            // Arrange
            var jsonBody = @"{
                        ""userId"": 12,
                        ""username"": ""theUser"",
                        ""email"": ""john@email.com"",
                        ""role"": ""Admin""
                    }";

            var request = new HttpRequestMessage(HttpMethod.Put, "/user");
            request.Headers.Add("Authorization", "Bearer " + _token);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutUser_ReturnsOk_WhenUserReturnedSuccessfully()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://developmentapi.deldiosmotorclubadmin.com");

            // Arrange
            var jsonBody = @"{
                        ""userId"": 1,
                        ""username"": ""theUser"",
                        ""email"": ""john@email.com"",
                        ""role"": ""Admin""
                    }";

            var request = new HttpRequestMessage(HttpMethod.Put, "/user");
            request.Headers.Add("Authorization", "Bearer " + _token);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }

}
