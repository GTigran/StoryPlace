using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace StoryPlace.DataLayer.BusinessObjects.Entities
{
    public class Story : BaseAuditEntity
    {
        public Story()
        {
            Groups = new HashSet<Group>();
        }

        #region Mapping Properties

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get;set;}

        
        public ICollection<Group> Groups { get; set; }

        public User.User User { get; set; }

        #endregion

        #region Out of Mapping
        [NotMapped]
        private List<int> _groupIds;
        

        [NotMapped]
        public List<int> GroupIDs
        {
            get
            {
                if (_groupIds == null)
                {
                    _groupIds = Groups != null
                        ? Groups.Select(a => a.ID).ToList() : null;
                }

                return _groupIds;
            }
            set
            {
                _groupIds = value;
            }
        }

#endregion Out of Mapping




    }
}
