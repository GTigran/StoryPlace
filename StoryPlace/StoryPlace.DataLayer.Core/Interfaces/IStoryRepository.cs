using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryPlace.DataLayer.BusinessObjects.Entities;

namespace StoryPlace.DataLayer.Core.Interfaces
{
    public interface IStoryRepository
    {
        IEnumerable<Story> GetUserStories(int userId);

        void UpsertStory(Story story);


        Story GetSingleStory(int id);
    }
}
