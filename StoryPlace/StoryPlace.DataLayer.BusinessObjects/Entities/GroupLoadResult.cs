using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryPlace.DataLayer.BusinessObjects.Entities
{
    [NotMapped]
    public class GroupLoadResult:Group
    {
        [NotMapped]
        public int UserCount { get; set; }

        [NotMapped]
        public int StoryCount { get; set; }

        [NotMapped]
        public int ? Joined { get; set; }

    }
}
