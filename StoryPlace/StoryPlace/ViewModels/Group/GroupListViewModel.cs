using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoryPlace.DataLayer.BusinessObjects.Entities;

namespace StoryPlace.Web.ViewModels.Group
{
    public class GroupListViewModel
    {
        public IEnumerable<GroupLoadResult> Items { get; set; }
    }


    public class GroupItemViewModel
    {
        public DataLayer.BusinessObjects.Entities.Group Group
        {
            get;set;
        }

        public int UserCount {get;set;}

        public int StoriesPublished {get;set;}

    }

}