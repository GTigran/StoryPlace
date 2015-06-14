using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StoryPlace.DataLayer.BusinessObjects.Entities;

namespace StoryPlace.DataLayer.Core.Interfaces
{
    public interface IBaseRepository<TContext,TEntity>
        where TContext : DbContext
        where TEntity : class, IDbEntity
    {
        TContext DbContext { get; set; }

        DbSet<TEntity> Set {get;set;} 

        
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null,string includeProperties = "");
        
        TEntity GetSingleElementById(object id);
        
        void Insert(TEntity entity,int userId = 0);
        
        void Delete(object id);
        
        void Delete(TEntity entityToDelete);
        
        void Update(TEntity entityToUpdate);
       
       IEnumerable<TReturn> ExecuteStoredProcedure<TReturn>(string spName, SqlParameter [] sqlParameters);
       

    }
}
