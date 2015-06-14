using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoryPlace.Web.ViewModels.Story
{
    public class StoryEditViewModel
    {
        public DataLayer.BusinessObjects.Entities.Story Story
        {
            get;
            set;
        }

        public IEnumerable<SelectListItem> GroupItems { get; set; }
    }
}