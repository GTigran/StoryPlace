using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoryPlace.DataLayer.Base;

namespace StoryPlace.DataLayer.Core.Tools
{
    public abstract class BaseUnitOfWork<TContext>:IUnitOfWork<TContext>
        where TContext : DbContext
    {

        public void Dispose()
        {
            
        }

        public abstract int Save();


        public abstract TContext Context
        {
            get;
            set;
        }
        
    }
}
