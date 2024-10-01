using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using PWPProject.Controllers; // Import the namespace where your controller resides

namespace VideoControllerTesting
{
    [TestClass]
    public class VideoControllerTests
    {
        [TestMethod]
        public void GetHomePageVideos_Returns_OK_With_Valid_Videos()
        {
            // Arrange
            var mockBusinessLogicLayer = new Mock<IBusinessLogicLayer>(); // Assuming IBusinessLogicLayer is the interface for your business logic layer
            var expectedVideos = new List<VideosDTO>(); // Define expected videos

            mockBusinessLogicLayer.Setup(bll => bll.GetHomePageVideos()).Returns(expectedVideos);
            var controller = new VideoController(mockBusinessLogicLayer.Object);

            // Act
            var result = controller.GetHomePageVideos() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Success", (result.Value as GetResponse<List<VideosDTO>>).Message);
            CollectionAssert.AreEqual(expectedVideos, (result.Value as GetResponse<List<VideosDTO>>).Data);
        }

        [TestMethod]
        public void GetHomePageVideos_Returns_InternalServerError_When_BusinessLogicLayer_Not_Initialized()
        {
            // Arrange
            var mockBusinessLogicLayer = new Mock<IBusinessLogicLayer>(); // Assuming IBusinessLogicLayer is the interface for your business logic layer
            mockBusinessLogicLayer.Setup(bll => bll.GetHomePageVideos()).Throws<InvalidOperationException>();
            var controller = new VideoController(null);

            // Act
            var result = controller.GetHomePageVideos() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Business Logic Layer is not initialized.", (result.Value as GetResponse<object>).Message);
        }

        [TestMethod]
        public void GetHomePageVideos_Returns_InternalServerError_For_Generic_Exception()
        {
            // Arrange
            var mockBusinessLogicLayer = new Mock<IBusinessLogicLayer>(); // Assuming IBusinessLogicLayer is the interface for your business logic layer
            mockBusinessLogicLayer.Setup(bll => bll.GetHomePageVideos()).Throws<Exception>();
            var controller = new VideoController(mockBusinessLogicLayer.Object);

            // Act
            var result = controller.GetHomePageVideos() as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Internal Server Error", (result.Value as GetResponse<object>).Message);
        }
    }
}
