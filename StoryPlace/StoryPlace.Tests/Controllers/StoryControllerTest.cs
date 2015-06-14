using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StoryPlace.DataLayer.BusinessObjects.Entities;
using StoryPlace.DataLayer.Core.Tools;
using StoryPlace.Web.Controllers;
using StoryPlace.Web.Tests.Repository;
using StoryPlace.Web.ViewModels.Story;

namespace StoryPlace.Web.Tests.Controllers
{
    [TestClass]
    public class StoryControllerTest
    {
        private readonly List<Story> _stories;
        private readonly StoryController _controller = null;

        public StoryControllerTest()
        {
            _stories = new List<Story>();
        for (var i = 1; i < 6;i++ )
        {
            _stories.Add(new Story
            {
                ID = i + 1,
                Content = string.Format("Story Content {0} ", i),
                Description = string.Format("Story Description {0}", i),
                Title = string.Format("Story Title {0}", i),
                CreatedDate = DateTime.Now,
            });
        }

            var repo = new DummyStoryRepository(_stories);

            var uow = new UnitOfWork(repo);

            // Now lets create the BooksController object to test and pass our unit of work
            _controller = new StoryController(uow);
        }

        [TestMethod]
        public void Index()
        {
            ViewResult result = _controller.Index() as ViewResult;

            // Now lets evrify whether the result contains our book entries or not
            var model = (StoryListViewModel)result.ViewData.Model;

            Assert.IsNotNull(model);
            
           
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
