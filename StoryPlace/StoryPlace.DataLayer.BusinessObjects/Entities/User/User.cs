using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace StoryPlace.DataLayer.BusinessObjects.Entities.User
{
    public sealed class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {

        #region Properties

        public ICollection<Group> Groups { get; set; }


        [ForeignKey("CreatedBy")]
        public ICollection<Story> Stories { get; set; }

        #endregion

        public User()
        {
        }
        public User(string name) : this() { UserName = name; }
    }

    

    public class Role : IdentityRole<int, UserRole>
    {
        public Role()
        {
        }
        public Role(string name) : this() { Name = name; }
    }

    public class UserRole : IdentityUserRole<int> { }

    public class UserClaim : IdentityUserClaim<int> { }

    public class UserLogin : IdentityUserLogin<int> { }

}
