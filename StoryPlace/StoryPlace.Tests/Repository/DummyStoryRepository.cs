using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StoryPlace.DataLayer.BusinessObjects.Entities;
using StoryPlace.DataLayer.Core.DBContexts;
using StoryPlace.DataLayer.Core.Interfaces;

namespace StoryPlace.Web.Tests.Repository
{
    public class DummyStoryRepository:IStoryRepository
    {

        List<Story> Stories { get; set; }


        public DummyStoryRepository(List<Story> stories)
        {
            Stories = stories;
        }

        public Story GetUserStories(int userId)
        {
            return Stories.FirstOrDefault(a => a.CreatedBy == userId);
        }

        IEnumerable<Story> IStoryRepository.GetUserStories(int userId)
        {
            return Stories.Where(a => a.CreatedBy == userId).ToList();
        }

        public void UpsertStory(Story story)
        {
            if (story.ID == 0)
            {
                Stories.Add(story);
            }
            else
            {
                var current = Stories.SingleOrDefault(a => a.ID == story.ID);

                Stories.Remove(current);
                Stories.Add(story);

            }
        }



        #region  Dummy

        public Story GetSingleStory(int id)
        {
            return Stories.Single(a => a.ID == id);
        }

        public CombinedDbContext DbContext
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public DbSet<Story> Set
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IEnumerable<Story> Get(Expression<Func<Story, bool>> filter = null, Func<IQueryable<Story>, IOrderedQueryable<Story>> orderBy = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public Story GetSingleElementById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Story entity, int userId = 0)
        {
            throw new NotImplementedException();
        }

        public void Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Delete(Story entityToDelete)
        {
            throw new NotImplementedException();
        }

        public void Update(Story entityToUpdate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TReturn> ExecuteStoredProcedure<TReturn>(string spName, SqlParameter[] sqlParameters)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
