using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using StoryPlace.DataLayer.Base;
using StoryPlace.DataLayer.BusinessObjects.Entities;

namespace StoryPlace.DataLayer.Core.DBContexts
{
    public class CombinedDbContext : BaseContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Story> Stories { get; set; }

       protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            #region Many To Many Relationship Definition for Group->Stories
            //mapping for story groups
            modelBuilder.Entity<Story>().HasMany(s => s.Groups)
                 .WithMany(p => p.Stories)
                 .Map(t =>
                 {
                     t.MapLeftKey("StoryID");
                     t.MapRightKey("GroupID");
                     t.ToTable("GroupStories");
                 });

            #endregion


            #region Many To Many Relationship Definition for Group->Users

            modelBuilder.Entity<Group>().HasMany(s => s.Users)
             .WithMany(p => p.Groups)
             .Map(t =>
             {
                 t.MapLeftKey("GroupID");
                 t.MapRightKey("UserID");
                 t.ToTable("GroupUsers");
             });

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
