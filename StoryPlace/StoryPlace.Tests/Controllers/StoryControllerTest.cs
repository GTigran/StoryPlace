using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryPlace.Web.Controllers;

namespace StoryPlace.Web.Tests.Controllers
{
    [TestClass]
    public class StoryControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            StoryController controller = new StoryController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void Edit()
        {
            // Arrange
            StoryController controller = new StoryController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
