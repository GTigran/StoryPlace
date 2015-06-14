using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StoryPlace.DataLayer.Base;
using StoryPlace.DataLayer.BusinessObjects.Entities;
using StoryPlace.DataLayer.BusinessObjects.Entities.User;
using StoryPlace.DataLayer.Core.DBContexts;
using StoryPlace.DataLayer.Core.Interfaces;
using StoryPlace.DataLayer.Core.Repositories;

namespace StoryPlace.DataLayer.Core.Tools
{
    public class UnitOfWork: IUnitOfWork<CombinedDbContext>
    {
        #region EF
        private readonly CombinedDbContext _storyPlacecontext;
        #endregion EF
        

        #region Repositories

        private IGroupRepository _groupRepository;
        private  IStoryRepository _storyRepository;

        /// <summary>
        /// Repository for quering items related to groups.
        /// </summary>
        public IGroupRepository GroupRepository
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = new GroupRepository(_storyPlacecontext);
                }

                return _groupRepository;

            }
        }


        /// <summary>
        /// Repository for quering items related to stories.
        /// </summary>
        public IStoryRepository StoryRepository
        {
            get
            {
                if (_storyRepository == null)
                {
                    _storyRepository = new StoryRepository(_storyPlacecontext);
                }

                return _storyRepository;

            }
        }


        #endregion Repositories


        #region Identity

        public int UserID { get; set; }

        private  UserManager<User,int> _userManager;


        public UserManager<User,int> UserManager
        {
            get
            {
                if (_userManager == null)
                {
                    _userManager = new UserManager<User, int>(new AppUserStore(new CombinedDbContext()));
                }


                return _userManager;
            }
        }

        
        #endregion



        #region Ctor

        public UnitOfWork()
        {
            _storyPlacecontext = new CombinedDbContext();
        }


        public UnitOfWork(IStoryRepository storyRepo)
        {
            _storyRepository = storyRepo;

        } 

        #endregion ctor

        /// <summary>
        /// Resolves audit entities before save
        /// </summary>
        private void ResolveAuditDetails()
        {

            #region Tracking newly added entires

            var addedEntries =
                _storyPlacecontext.ChangeTracker.Entries().
                Where(a => a.State == EntityState.Added);

            foreach (var entity in addedEntries)
            {
                var auditEntity = entity.Entity as BaseAuditEntity;
                if (auditEntity != null)
                  {
                      auditEntity.CreatedDate = DateTime.Now;
                      auditEntity.CreatedBy = UserID;
                  }
            }

            var modifiedEntries = _storyPlacecontext.ChangeTracker.Entries()
                .Where(a => a.State == EntityState.Modified);

            #endregion Tracking newly added entires

            #region Tracking modified audit items

            foreach (var entity in modifiedEntries)
            {
                var auditEntity = entity.Entity as BaseAuditEntity;

                if (auditEntity != null)
                {

                    var entry = _storyPlacecontext.Entry(auditEntity);
                    entry.Property(e => e.CreatedBy).IsModified = false;
                    entry.Property(e => e.CreatedDate).IsModified = false;
                }
            }

            #endregion Tracking modified audit items
        }


        /// <summary>
        /// Save repository changes if they are in place
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            
            if(_storyPlacecontext.ChangeTracker.HasChanges())
            {
                ResolveAuditDetails();
                return _storyPlacecontext.SaveChanges();
            }

            return 0;

            
        }

        /// <summary>
        /// 
        /// </summary>
        public CombinedDbContext Context
        {
            get
            {
                return _storyPlacecontext;
            }
        }


        public void Dispose()
        {
            _storyPlacecontext.Dispose();
        }
    }
}
