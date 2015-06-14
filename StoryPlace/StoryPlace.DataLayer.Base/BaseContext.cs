using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using StoryPlace.DataLayer.BusinessObjects.Entities.User;

namespace StoryPlace.DataLayer.Base
{
    /// <summary>
    /// Base Db Context for Int Based Identity 
    /// </summary>
    public class BaseContext : IdentityDbContext<User,Role, int, UserLogin, UserRole, UserClaim>
    {
        
        protected BaseContext()
            : base("name=StoryPlaceDB")
        {
            
        }
    }
}
