using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestxUnit
{
    public class UnitTest1
    {
        private readonly HttpClient _httpClient;

        private readonly string validAdminToken =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9oYXNoIjoiMTgiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6InRhbGhhIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoidGFsaGFAZ21haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoidGFsaGEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTgxOTAwMzI1MSwiaXNzIjoiaHR0cDovL05BLyIsImF1ZCI6Imh0dHA6Ly9OQS8ifQ.H_cvNCl8HRCgaX274qfMfG97XRCBHUl0gJ-OwC034_I";

        private readonly string invalidToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA" +
                                               "1L2lkZW50aXR5L2NsYWltcy9oYXNoIjoiMSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMD" +
                                               "UvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoidGFsaGEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm" +
                                               "9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0YWxoYUBpYmV4LmNvIiwiaHR0cDovL3Nja" +
                                               "GVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoidGFsaGEiLCJodHRwOi8vc2N" +
                                               "oZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsI" +
                                               "CI6MTgxNzg1MzY5OCwiaXNzIjoiaHR0cDovL05BLyIsImF1ZCI6Imh0dHA6Ly9OQS8ifQ.6jIHkjapjkpiQuzBYSMx" +
                                               "OulagriROx5dA64tzBXFu7c";

        private readonly string validUserToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9oYXNoIjoiMiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoidGFsaGEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJ0YWxoYUBpYmV4LmNvIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZ2l2ZW5uYW1lIjoidGFsaGEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJVc2VyIiwiZXhwIjoxODE4MTEwMzY2LCJpc3MiOiJodHRwOi8vTkEvIiwiYXVkIjoiaHR0cDovL05BLyJ9.4f-tsOU2TXjPbqyclzyfZinfRSbgUK2GHfT9adGVtPE";

        private readonly string validVideoId = "d5b4e99a-16f6-44f0-adf6-81807798f0db";
        private readonly string validId = "04d0260f-7323-4268-b9f7-44779c8a1ab3";
        private readonly string newVideoId = "012345678";
        private readonly int validUserId = 2;
        private readonly int validVoteType = 3; // within the range of 1 to 5


        /// <summary>
        /// Change before next test run
        /// </summary>
        private readonly string newuUserEmail = "newemail191";
        private readonly string newVideoIdfromDb = "438e2483-74a6-4c5a-ba79-adb1496d3fe2";
        private readonly int deleteUser = 1020;


        public class EditVideo
        {
            public string Id { get; set; }

            public string? Tags { get; set; }

            public string? Description { get; set; }

        }

        public UnitTest1()
        {
            // Arrange
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7130")
            };
        }

        #region Videos

        /// <summary>
        /// Tests for GET Videos
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task GetVideos_AllVideos_ReturnsSuccessCode()
        {
            // Act
            var response = await _httpClient.GetAsync("/videos");

            // Assert
            response.EnsureSuccessStatusCode(); // Asserts that the status code is 2xx

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseContent); // Asserts that the response content is not nul
        }

        [Fact]
        public async Task GetVideos_AllVideos_ReturnsNoNullContent()
        {
            // Act
            var response = await _httpClient.GetAsync("/videos");

            // Assert

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseContent); // Asserts that the response content is not nul
        }

        /// <summary>
        /// Tests for POST Videos
        /// </summary>
        /// <returns></returns>

        [Fact]
        public async Task PostVideo_AllFields_ValidAdminToken_ReturnsSuccess()
        {
            // Arrange
            var video = new { videoId = "9361f795-ea9f-48f0-b423-2c6d8d3b5281", tags = "tag1, tag2", description = "Sample video" };
            var content = new StringContent(JsonConvert.SerializeObject(video), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.PostAsync("/videos", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostVideo_MissingToken_ReturnsUnauthorized()
        {
            // Arrange
            var video = new { videoId = "9361f795-ea9f-48f0-b423-2c6d8d3b5281", tags = "tag1, tag2", description = "Sample video" };
            var content = new StringContent(JsonConvert.SerializeObject(video), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/videos", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostVideo_InvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            var video = new { videoId = "f958ca54-cf25-4557-b287-86265dc93a34", tags = "tag1, tag2", description = "Sample video" };
            var content = new StringContent(JsonConvert.SerializeObject(video), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);

            // Act
            var response = await _httpClient.PostAsync("/videos", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostVideo_NonAdminToken_ReturnsForbidden()
        {
            // Arrange
            var video = new { videoId = "f958ca54-cf25-4557-b287-86265dc93a34", tags = "tag1, tag2", description = "Sample video" };
            var content = new StringContent(JsonConvert.SerializeObject(video), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.PostAsync("/videos", content);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task PostVideo_OnlyVideoId_ValidAdminToken_ReturnsSuccess()
        {
            // Arrange
            var video = new { videoId = newVideoId, tags = (string)null, description = (string)null };
            var content = new StringContent(JsonConvert.SerializeObject(video), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.PostAsync("/videos", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostVideo_EmptyVideoId_ValidAdminToken_ReturnsBadRequest()
        {
            // Arrange
            var video = new { videoId = "", tags = "tag1, tag2", description = "Sample video" };
            var content = new StringContent(JsonConvert.SerializeObject(video), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.PostAsync("/videos", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostVideo_EmptyTagsAndDescription_ValidAdminToken_ReturnsSuccess()
        {
            // Arrange
            var video = new { videoId = Guid.NewGuid().ToString(), tags = "", description = "" };
            var content = new StringContent(JsonConvert.SerializeObject(video), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.PostAsync("/videos", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostVideo_InvalidJsonStructure_ValidAdminToken_ReturnsBadRequest()
        {
            // Arrange
            var content = new StringContent("{ videoId: \"123\", tags: \"tag1, tag2\", description: \"Sample video\" ", Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.PostAsync("/videos", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region Vote
        [Fact]
        public async Task PostVote_ValidAdminToken_ReturnsSuccess()
        {
            // Arrange
            var vote = new { videoId = validId, isBookMarked = true, voteType = validVoteType };
            var content = new StringContent(JsonConvert.SerializeObject(vote), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.PostAsync("/vote", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostVote_ValidUserToken_ReturnsSuccess()
        {
            // Arrange
            var vote = new { videoId = validId, isBookMarked = true, voteType = validVoteType };
            var content = new StringContent(JsonConvert.SerializeObject(vote), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.PostAsync("/vote", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostVote_MissingToken_ReturnsUnauthorized()
        {
            // Arrange
            var vote = new { videoId = validVideoId, isBookMarked = true, voteType = validVoteType };
            var content = new StringContent(JsonConvert.SerializeObject(vote), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/vote", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostVote_InvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            var vote = new { videoId = validVideoId, isBookMarked = true, voteType = validVoteType };
            var content = new StringContent(JsonConvert.SerializeObject(vote), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);

            // Act
            var response = await _httpClient.PostAsync("/vote", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostVote_NonAdminNonUserToken_ReturnsForbidden()
        {
            // Arrange
            var vote = new { videoId = validVideoId, isBookMarked = true, voteType = validVoteType };
            var content = new StringContent(JsonConvert.SerializeObject(vote), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);

            // Act
            var response = await _httpClient.PostAsync("/vote", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostVote_InvalidVideoId_ReturnsNotFound()
        {
            // Arrange
            var invalidVideoId = "invalid-video-id";
            var vote = new { videoId = invalidVideoId, isBookMarked = true, voteType = validVoteType };
            var content = new StringContent(JsonConvert.SerializeObject(vote), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.PostAsync("/vote", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostVote_OutOfRangeVoteType_ReturnsBadRequest()
        {
            // Arrange
            var outOfRangeVoteType = 6; // invalid, since valid range is 1 to 5
            var vote = new { videoId = validVideoId, isBookMarked = true, voteType = outOfRangeVoteType };
            var content = new StringContent(JsonConvert.SerializeObject(vote), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.PostAsync("/vote", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostVote_MissingVideoId_ReturnsBadRequest()
        {
            // Arrange
            var vote = new { videoId = (string)null, isBookMarked = true, voteType = validVoteType };
            var content = new StringContent(JsonConvert.SerializeObject(vote), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.PostAsync("/vote", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostVote_MissingVoteType_ReturnsNotFound()
        {
            // Arrange
            var vote = new { videoId = validVideoId, isBookMarked = true, voteType = (int?)null };
            var content = new StringContent(JsonConvert.SerializeObject(vote), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.PostAsync("/vote", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        #endregion

        #region Video

        [Fact]
        public async Task GetVideo_ValidId_ReturnsVideo()
        {
            // Arrange
            var userId = validUserId; // Optional query parameter
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.GetAsync($"/video/{validId}?userId={userId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task GetVideo_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidId = "invalid-id";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.GetAsync($"/videos/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetVideo_WithUserIdWithVideoId_ReturnsVideo()
        {
            // Arrange
            var userId = "1"; // Optional query parameter

            // Act
            var response = await _httpClient.GetAsync($"/video/{validId}?userId={userId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // ** PUT /videos **

        [Fact]
        public async Task EditVideo_ValidAdminToken_ReturnsSuccess()
        {
            // Arrange
            var editVideo = new EditVideo
            {
                Id = validId,
                Tags = "New Tag",
                Description = "Updated description"
            };

            var content = new StringContent(JsonConvert.SerializeObject(editVideo), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.PutAsync("/video", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ApproveVideo_InvalidAdminToken_ReturnsUnauthorized()
        {
            // Arrange
            var editVideo = new EditVideo
            {
                Id = validVideoId,
                Tags = "New Tag",
                Description = "Updated description"
            };
            var content = new StringContent(JsonConvert.SerializeObject(editVideo), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);

            // Act
            var response = await _httpClient.PutAsync("/video", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task ApproveVideo_MissingToken_ReturnsUnauthorized()
        {
            // Arrange
            var editVideo = new EditVideo
            {
                Id = validVideoId,
                Tags = "New Tag",
                Description = "Updated description"
            };
            var content = new StringContent(JsonConvert.SerializeObject(editVideo), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync("/video", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task ApproveVideo_NonAdminToken_ReturnsForbidden()
        {
            // Arrange
            var editVideo = new EditVideo
            {
                Id = validVideoId,
                Tags = "New Tag",
                Description = "Updated description"
            };
            var content = new StringContent(JsonConvert.SerializeObject(editVideo), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.PutAsync("/video", content);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        // ** DELETE /videos/{id} **

        [Fact]
        public async Task DeleteVideo_ValidAdminToken_ReturnsSuccess()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.DeleteAsync($"/video/{newVideoIdfromDb}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteVideo_MissingToken_ReturnsUnauthorized()
        {
            // Act
            var response = await _httpClient.DeleteAsync($"/video/{validVideoId}");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task DeleteVideo_InvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);

            // Act
            var response = await _httpClient.DeleteAsync($"/video/{validVideoId}");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task DeleteVideo_NonAdminToken_ReturnsForbidden()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.DeleteAsync($"/video/{validVideoId}");

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task DeleteVideo_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidId = "invalid-id";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.DeleteAsync($"/video/{invalidId}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        #endregion

        #region Stats 

        [Fact]
        public async Task Get_ValidStats_ReturnsOk()
        {

            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.GetAsync($"/Stats");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion

        #region Bookmark
        [Fact]
        public async Task PostVideoBookmark_ValidRequest_ReturnsOk()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);
            var videoAction = new
            {
                VideoId = validId,
                IsBookMarked = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(videoAction), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/Bookmark", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostVideoBookmark_InvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);
            var videoAction = new
            {
                VideoId = validId,
                IsBookMarked = true
            };
            var content = new StringContent(JsonConvert.SerializeObject(videoAction), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/Bookmark", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task PostVideoBookmark_InvalidRequest_ReturnsNotFound()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);
            var videoAction = new
            {
                VideoId = "8896972000",
                IsBookMarked = true
            };

            var content = new StringContent(JsonConvert.SerializeObject(videoAction), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/Bookmark", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task EditVideoBookmark_InvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);
            var videoAction = new
            {
                VideoId = validId,
                IsBookMarked = false
            };
            var content = new StringContent(JsonConvert.SerializeObject(videoAction), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync("/Bookmark", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task EditVideoBookmark_InvalidRequest_ReturnsNotFound()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);
            var videoAction = new
            {
                VideoId = validId,
                IsBookMarked = false
            };
            var content = new StringContent(JsonConvert.SerializeObject(videoAction), Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PutAsync("/Bookmark", content);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        #endregion

        #region User
        [Fact]
        public async Task GetUser_ValidAdminToken_ReturnsSuccess()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, $"/User/{validUserId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Asserts that the status code is 2xx
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseContent); // Asserts that the response content is not null
        }

        [Fact]
        public async Task GetUser_InvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, $"/User/{validUserId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode); // Asserts that the status code is 401
        }

        [Fact]
        public async Task GetUser_ValidUserToken_ReturnsForbidden()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Get, $"/User/{validUserId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode); // Asserts that the status code is 403
        }

        [Fact]
        public async Task DeleteUser_InvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/User/{validUserId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode); // Asserts that the status code is 401
        }

        [Fact]
        public async Task DeleteUser_ValidUserToken_ReturnsForbidden()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/User/{validUserId}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode); // Asserts that the status code is 403
        }

        [Fact]
        public async Task EditUser_ValidAdminToken_ReturnsSuccess()
        {
            // Arrange
            var editUserDto = new 
            {
                UserId = validUserId,
                Username = "EditedUsername",
                Email = "editedemail@example.com",
                Role = "Admin" // Or whatever role makes sense in your context
            };

            var content = new StringContent(JsonConvert.SerializeObject(editUserDto), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, "/User")
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Asserts that the status code is 2xx

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode); // Asserts that the response content is not null

        }

        [Fact]
        public async Task EditUser_InvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            var editUserDto = new 
            {
                UserId = validUserId,
                Username = "EditedUsername",
                Email = "editedemail@example.com",
                Role = "Admin"
            };

            var content = new StringContent(JsonConvert.SerializeObject(editUserDto), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, "/User")
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", invalidToken);

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode); // Asserts that the status code is 401
        }

        [Fact]
        public async Task EditUser_ValidUserToken_ReturnsForbidden()
        {
            // Arrange
            var editUserDto = new 
            {
                UserId = validUserId,
                Username = "EditedUsername",
                Email = "editedemail@example.com",
                Role = "User" // Assuming the user is not allowed to change roles or certain fields
            };

            var content = new StringContent(JsonConvert.SerializeObject(editUserDto), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, "/User")
            {
                Content = content
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", validUserToken);

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode); // Asserts that the status code is 403
        }

        #endregion

        #region Users

        [Fact]
        public async Task PostUser_ValidUser_ReturnsCreated()
        {
            // Arrange
            var newUser = new 
            {
                Email = newuUserEmail,
                Username = "newuser",
                Password = "Password123!"
            };

            var jsonContent = JsonConvert.SerializeObject(newUser);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/Users", content);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        }

        [Fact]
        public async Task PostUser_ExistingUser_ReturnsConflict()
        {
            // Arrange
            var existingUser = new 
            {
                Email = "existinguser@example.com",
                Username = "existinguser",
                Password = "Password123!"
            };

            var jsonContent = JsonConvert.SerializeObject(existingUser);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // First post to create the user
            await _httpClient.PostAsync("/Users", content);

            // Act - Try to create the same user again
            var response = await _httpClient.PostAsync("/Users", content);

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseContent);

        }

        [Fact]
        public async Task PostUser_InvalidUser_ReturnsInternalServerError()
        {
            // Arrange
            var invalidUser = new 
            {
                Email = "", // Invalid Email
                Username = "invaliduser",
                Password = "Password123!"
            };

            var jsonContent = JsonConvert.SerializeObject(invalidUser);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Act
            var response = await _httpClient.PostAsync("/Users", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task GetAllUsers_AsAdmin_ReturnsOk()
        {
            // Arrange
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.GetAsync("/Users");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.NotNull(responseContent);
        }

        [Fact]
        public async Task GetAllUsers_AsNonAdmin_ReturnsUnauthorized()
        {
            // Arrange
            // Do not set any token or set a non-admin token

            // Act
            var response = await _httpClient.GetAsync("/Users");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_ValidAdminToken_ReturnsSuccess()
        {
            // Arrange
            var request = new HttpRequestMessage(HttpMethod.Delete, $"/User/{deleteUser}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", validAdminToken);

            // Act
            var response = await _httpClient.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Asserts that the status code is 2xx
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        #endregion
    }
}