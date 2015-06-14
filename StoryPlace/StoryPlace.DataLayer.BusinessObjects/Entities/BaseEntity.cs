using System;
using System.ComponentModel.DataAnnotations;

namespace StoryPlace.DataLayer.BusinessObjects.Entities
{


    /// <summary>
    /// Base 
    /// </summary>
    public class BaseDBntity : IDbEntity
    {

        [Key]
        [Required]
        public int ID
        {
            get ; set;
        }
    }


    public class BaseAuditEntity : BaseDBntity
    {
        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }
        
    }


}
