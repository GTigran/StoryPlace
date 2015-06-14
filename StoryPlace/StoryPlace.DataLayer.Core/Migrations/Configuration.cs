using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using StoryPlace.DataLayer.BusinessObjects.Entities;
using StoryPlace.DataLayer.BusinessObjects.Entities.User;
using StoryPlace.DataLayer.Core.DBContexts;

namespace StoryPlace.DataLayer.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StoryPlace.DataLayer.Core.DBContexts.CombinedDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "StoryPlace.DataLayer.Core.DBContexts.CombinedDbContext";
        }

        protected override void Seed(StoryPlace.DataLayer.Core.DBContexts.CombinedDbContext context)
        {

            #region Creating Initial Users
            var manager = new UserManager<User,int>(
                new Tools.AppUserStore(
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


            for (var i = 0; i < 4;i++ )
            {
                var newGroup = new Group
                {
                    ID=i+1,
                    Name = string.Format("Group Name {0}" , i),
                    Description = string.Format("Story Description {0}" , i)
                };
                context.Groups.AddOrUpdate(x=>x.ID,
                    newGroup
                );

                createdGroups.Add(newGroup);
                
            }


            #endregion Adding Sample Groups

            #region Adding Sample Stories


            for (var i = 0; i < 4;i++ )
            {
                context.Stories.AddOrUpdate(x=>x.ID,
                new Story
                {
                    ID=i+1,
                    Content = string.Format("Story Content {0} " ,i),
                    Description = string.Format("Story Description {0}" , i),
                    Title = string.Format("Story Title {0}" , i),
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


    public class StoryPlaceMigration : DbMigration
    {
        public override void Up()
        {
           // This command executes the SQL you have written
            // to create the stored procedures
            Sql(InstallScript);

            // or, to alter stored procedures
            Sql(AlterScript);
        }

        public override void Down()
        {

            var initialSql = @"CREATE  PROCEDURE GetGroupInfo
	(
		@userID INT
	)
AS
BEGIN

	SELECT [Name],[Description],ID, (SELECT COUNT(1)
	FROM dbo.GroupStories WHERE GroupID = g.ID) AS StoryCount,
	(SELECT COUNT(1)
	FROM dbo.GroupUsers WHERE GroupID = g.ID)
	AS UserCount ,
	(SELECT 1 FROM dbo.GroupUsers WHERE GroupID = g.ID
	AND UserID = @userID) AS Joined
	
  FROM dbo.Groups g
	

END
GO
";
            Sql(initialSql);

            /*Sql(RollbackScript);*/
        }



        private const string InstallScript = @"
        CREATE PROCEDURE [dbo].[{0}]
        {1}
    ";

        private const string UninstallScript = @"
        DROP PROCEDURE [dbo].[MyProcedure];
    ";

        // or for alters
        private const string AlterScript = @"
        ALTER PROCEDURE [dbo].[AnotherProcedure]
        ... Newer SP logic here ...
    ";

        private const string RollbackScript = @"
        ALTER PROCEDURE [dbo].[AnotherProcedure]
        ... Previous / Old SP logic here ...
    ";
    }

}