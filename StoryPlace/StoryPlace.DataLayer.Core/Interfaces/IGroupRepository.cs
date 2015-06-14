using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryPlace.DataLayer.BusinessObjects.Entities;

namespace StoryPlace.DataLayer.Core.Interfaces
{
    public interface IGroupRepository
    {
        IEnumerable<GroupLoadResult> GetGroupList(int userId);

        void JoinUnJoin(int userId, int groupId);
        
    }
}
