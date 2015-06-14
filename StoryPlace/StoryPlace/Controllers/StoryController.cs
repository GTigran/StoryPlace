using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using StoryPlace.DataLayer.BusinessObjects.Entities;
using StoryPlace.DataLayer.Core.Tools;
using StoryPlace.Web.ViewModels.Story;

namespace StoryPlace.Web.Controllers
{
    [Authorize]
    public class StoryController:BaseController
    {
        public StoryController()
        {
            
        }

        public StoryController(UnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }


        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new StoryListViewModel
            {
                Stories = UnitOfWork.StoryRepository.GetUserStories(UserID),
            };
                
            return View(viewModel);
        }


        [HttpGet]
        public ViewResult Edit(int ? id)
        {
            //getting story from repository

            var story = id.HasValue ?
                UnitOfWork.StoryRepository.GetSingleStory(id.Value)
                : new Story();
            
            //getting all the groups
            var groups = UnitOfWork.GroupRepository.Get();

            var groupItems = 
                groups.Select(a => new SelectListItem
            {
                Selected = story.GroupIDs.Any(groupId => story.Groups.Any(g1 => g1.ID == groupId)),
                Text = a.Name,
                Value = a.ID.ToString()
            });

            
            var viewModel = new StoryEditViewModel
            {
                Story = story,
                GroupItems = groupItems
            };

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult Edit(StoryEditViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                UnitOfWork.StoryRepository.UpsertStory(viewModel.Story);

                return RedirectToAction("Index");
                
            }

            return View(viewModel);
        }

        public ActionResult Detail(int id)
        {
            var story = UnitOfWork.StoryRepository.GetSingleElementById(id);

            return View(story);
        }
    }
}