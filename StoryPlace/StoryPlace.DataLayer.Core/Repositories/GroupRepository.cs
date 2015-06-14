using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryPlace.DataLayer.BusinessObjects.Entities;
using StoryPlace.DataLayer.Core.DBContexts;
using StoryPlace.DataLayer.Core.Interfaces;

namespace StoryPlace.DataLayer.Core.Repositories
{
    public class GroupRepository : RepositoryBase<CombinedDbContext, Group>,
        IGroupRepository
    {

        #region ctor
        public GroupRepository(CombinedDbContext context) : base(context)
        {
        }

        #endregion ctor
        
        #region Impl

        /// <summary>
        /// Gets grup list with its related data
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<GroupLoadResult> GetGroupList(int userId)
        {
            var param = new []{new SqlParameter("userID",userId)}  ;
            
            return ExecuteStoredProcedure<GroupLoadResult>("[dbo].[GetGroupInfo]", param);
        }


        /// <summary>
        /// Togles join state for the group for the given user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="groupId"></param>
        public void JoinUnJoin(int userId,int groupId)
        {
            var item = DbContext.Groups.Include("Users").FirstOrDefault(a => a.ID == groupId);
            var user = DbContext.Users.FirstOrDefault(a => a.Id == userId);

            if (item != null)
            {
                
                var hasJoined = item.Users.Any(a => a.Id == userId);
                

                if (hasJoined)
                {
                    item.Users.Remove(user);
                }
                else 
                {
                    item.Users.Add(user);
                }
            }

        }

        #endregion
    }
}
