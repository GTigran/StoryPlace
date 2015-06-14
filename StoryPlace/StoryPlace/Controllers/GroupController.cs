using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoryPlace.DataLayer.BusinessObjects.Entities;
using StoryPlace.Web.ViewModels.Group;

namespace StoryPlace.Web.Controllers
{
    [Authorize]
    public class GroupController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var groups = UnitOfWork.GroupRepository.GetGroupList(UserID);

            var viewModel = new GroupListViewModel
            {
                Items = groups
            };

            return View(viewModel);
        }

        [HttpGet]
        public ViewResult Edit(int? id)
        {
            var group = id.HasValue ?
                UnitOfWork.GroupRepository.GetSingleElementById(id)
                : new Group();

            return View(group);

        }

        [HttpPost]
        public ActionResult Edit(Group model)
        {
            if (ModelState.IsValid)
            {
                if(model.ID>0)
                {
                    UnitOfWork.GroupRepository.Update(model);
                }
                else
                {
                    UnitOfWork.GroupRepository.Insert(model);
                }

                return  RedirectToAction("Index");
            }

            return View(model);

        }

        public ActionResult Join(int id)
        {
            UnitOfWork.GroupRepository.JoinUnJoin(UserID,id);

            return RedirectToAction("Index");
        }
    }
}