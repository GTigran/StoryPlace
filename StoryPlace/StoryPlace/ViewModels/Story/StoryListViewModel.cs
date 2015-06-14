using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace StoryPlace.Web.ViewModels.Story
{
    public class StoryListViewModel
    {
        public IEnumerable<DataLayer.BusinessObjects.Entities.Story> Stories
        {
            get;
            set;
        }
    }
}