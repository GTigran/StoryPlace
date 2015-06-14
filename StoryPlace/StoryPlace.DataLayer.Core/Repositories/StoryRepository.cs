using System;
using System.Collections.Generic;
using System.Linq;
using StoryPlace.DataLayer.BusinessObjects.Entities;
using StoryPlace.DataLayer.Core.DBContexts;
using StoryPlace.DataLayer.Core.Interfaces;

namespace StoryPlace.DataLayer.Core.Repositories
{
    public class StoryRepository : RepositoryBase<CombinedDbContext, Story>, IStoryRepository
    {

        #region ctor

        public StoryRepository(CombinedDbContext context) : base(context)
        {
        }

        #endregion ctor
        
        #region Impl


        /// <summary>
        /// Gets user entities by user 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Story> GetUserStories(int userId)
        {
            var id = userId;
            return Get(a => a.CreatedBy == id);
        }


        /// <summary>
        /// Gets single story along with its groups
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Story GetSingleStory(int id)
        {
            return DbContext.Stories.Include("Groups").FirstOrDefault(a => a.ID == id);
        }



        public void UpsertStory(Story story)
        {
            //getting db groups, that user have selected.
            var groups = (from g in DbContext.Groups
                          join groupId in story.GroupIDs
                              on g.ID equals groupId
                          select g).ToList();

            //if model ID >0 then updating the entru, otherwise inserting new one.
            if (story.ID > 0)
            {
                Update(story);

                DbContext.Entry(story).Collection(a => a.Groups).Load();

                story.Groups.Clear();
            }
            else
            {
                Insert(story);
            }

            foreach (var group in groups)
            {
                story.Groups.Add(group);
            }
        }


        #endregion Impl
        
    }

   
}
