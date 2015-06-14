using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryPlace.DataLayer.BusinessObjects.Entities
{
    public class GroupStory
    {
        public int GroupID { get; set; }

        public int StoryID { get; set; }
    }
}
