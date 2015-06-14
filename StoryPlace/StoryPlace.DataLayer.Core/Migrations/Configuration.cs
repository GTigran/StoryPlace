using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using StoryPlace.DataLayer.BusinessObjects.Entities;
using StoryPlace.DataLayer.BusinessObjects.Entities.User;
using StoryPlace.DataLayer.Core.DBContexts;
using StoryPlace.DataLayer.Core.Tools;

namespace StoryPlace.DataLayer.Core.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CombinedDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CombinedDbContext context)
        {

            #region Creating Initial Users
            var manager = new UserManager<User, int>(
                new AppUserStore(
                    new CombinedDbContext()));

            List<int> newlyCreatedUsers =
                new List<int>();
            // Create 4 test users:
            for (int i = 0; i < 4; i++)
            {
                var user = new User()
                {
                    UserName = string.Format("AppUser{0}", i)
                };

                manager.Create(user, string.Format("Password{0}", i));

                newlyCreatedUsers.Add(user.Id);
            }

            #endregion Creating Initial Users

            #region Adding Sample Groups

            var createdGroups = new List<Group>();


            for (var i = 0; i < 4; i++)
            {
                var newGroup = new Group
                {
                    ID = i + 1,
                    Name = string.Format("Group Name {0}", i),
                    Description = string.Format("Story Description {0}", i)
                };
                context.Groups.AddOrUpdate(x => x.ID,
                    newGroup
                );

                createdGroups.Add(newGroup);

            }


            #endregion Adding Sample Groups

            #region Adding Sample Stories


            for (var i = 0; i < 4; i++)
            {
                context.Stories.AddOrUpdate(x => x.ID,
                new Story
                {
                    ID = i + 1,
                    Content = string.Format("Story Content {0} ", i),
                    Description = string.Format("Story Description {0}", i),
                    Title = string.Format("Story Title {0}", i),
                    CreatedBy = newlyCreatedUsers[i],
                    CreatedDate = DateTime.Now,
                    Groups = new List<Group>
                    {
                      createdGroups[i]
                    }

                });
            }


            #endregion Adding Sample Stories

            context.SaveChanges();
        }
    }
}
