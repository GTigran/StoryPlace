using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using StoryPlace.DataLayer.BusinessObjects.Entities;
using StoryPlace.DataLayer.BusinessObjects.Entities.User;

namespace StoryPlace.DataLayer.Core.DBContexts
{
    public class UserDbcontext : IdentityDbContext<User,
        Role, int, UserLogin, UserRole, UserClaim>
    {
        public UserDbcontext()
            : base("name=StoryPlaceDB")
        {
            
        }
    }
}
