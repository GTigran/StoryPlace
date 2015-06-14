using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using StoryPlace.DataLayer.BusinessObjects.Entities;

namespace StoryPlace.DataLayer.Core.Repositories
{

    /// <summary>
    /// Base Repository
    /// Encapsulates all common  functionality for all repositories.
    /// </summary>
    public abstract class RepositoryBase<TContext,TEntity>
            where TContext : DbContext
            where TEntity  : class, IDbEntity
    {


        #region EF

        protected TContext DbContext { get; set; }

        protected DbSet<TEntity> Set ;

        #endregion EF

        #region ctor

        protected RepositoryBase(TContext context)
        {
            DbContext = context;
            this.Set = context.Set<TEntity>();
        }

        #endregion ctor
        
        #region IDisposable

        #endregion IDisposable

        #region Base Functionality

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,Func<IQueryable<TEntity>, 
            IOrderedQueryable<TEntity>> orderBy = null,string includeProperties = "")
        {
            IQueryable<TEntity> query = Set;
            
            #region Filter

            if (filter != null)
            {
                query = query.Where(filter);
            }

            #endregion Filter

            #region Loading Navigation Properties

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            #endregion

            #region Order by
                if( orderBy != null )
                {
                    return  orderBy(query).ToList();
                }

            #endregion
          
            return  query.ToList();
        }

        public virtual TEntity GetSingleElementById(object id)
        {
            return Set.Find(id);
        }

        public virtual void Insert(TEntity entity,int userId = 0)
        {
            Set.Add(entity);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = Set.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                Set.Attach(entityToDelete);
            }
            Set.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
           Set.Attach(entityToUpdate);

            
           DbContext.Entry(entityToUpdate).State = EntityState.Modified;
            
        }

       /* private void ResolveAuditDetails(TEntity entity,int userId=0)
        {
            var auditEntity = entity as BaseAuditEntity;

            if (auditEntity?.ID == 0)
            {
                auditEntity.CreatedDate = DateTime.Now;
                auditEntity.CreatedBy = userId;
            }
        }*/

        #endregion

        #region Helper Methods working with  stored Procedure


        /// <summary>
        /// executes stored procedure and return list of results
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="spName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        public IEnumerable<TReturn> ExecuteStoredProcedure<TReturn>(string spName, SqlParameter [] sqlParameters)
        {

            var expression = new  StringBuilder();

            expression.Append(spName);
            expression.Append(" ");
            
            foreach (var param in sqlParameters)
            {
                expression.AppendFormat(" @{0} ", param.ParameterName);
            }

            return this.DbContext.Database.SqlQuery<TReturn>(expression.ToString(),sqlParameters).ToList();

        }

        #endregion Helper Methods working with  stored Procedure



    }
}
