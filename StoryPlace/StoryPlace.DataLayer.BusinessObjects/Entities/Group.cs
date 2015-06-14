using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoryPlace.DataLayer.BusinessObjects.Entities
{
    public class Group : BaseDBntity
    {
        #region Mapping Properties
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Story> Stories { get; set; }

        public ICollection<User.User> Users { get; set; }

        #endregion Mapping Properties
    }
}
