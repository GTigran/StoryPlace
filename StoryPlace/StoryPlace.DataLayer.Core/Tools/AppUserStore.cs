using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using StoryPlace.DataLayer.BusinessObjects.Entities.User;

namespace StoryPlace.DataLayer.Core.Tools
{
    public class AppUserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public AppUserStore(DbContext context)
            : base(context)
        {

        }
    }

    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(DbContext context)
            : base(context)
        {
        }
    }
}
