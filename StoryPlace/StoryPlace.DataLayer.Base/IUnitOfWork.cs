using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryPlace.DataLayer.Base
{
    public interface IUnitOfWork<out TContext> : IDisposable
            where TContext : DbContext
            
    {
        int Save();

        TContext Context { get;  }

    }
}
