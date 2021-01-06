using LiteDB;
using Microsoft.Extensions.Options;
using TodoAgility.Persistence.Framework.ReadModel.Projections;

namespace TodoAgility.Persistence.ReadModel.Projections
{
    public class TodoAgilityProjectionsDbContext:ProjectionDbContext
    {
        public TodoAgilityProjectionsDbContext(IOptions<ProjectionDbOptions> options)
        :base(options)
        {
            OnModelCreating(BsonMapper.Global);
            Activities = Database.GetCollection<ActivityProjection>("activity");
            Projects = Database.GetCollection<ProjectProjection>("project");
        }
        
        public ILiteCollection<ActivityProjection> Activities { get; }
        
        public ILiteCollection<ProjectProjection> Projects { get; }
        
        private void OnModelCreating(BsonMapper modelBuilder)
        {
            #region ConfigureView

            modelBuilder.Entity<ActivityProjection>()
                .Field(k => k.ActivityId,"activityId")
                .Field(p=> p.ProjectId, "projectId")
                .Field(p=> p.Description,"description")
                .Field(p=> p.Status, "status")
                ;
            
            modelBuilder.Entity<ProjectProjection>()
                .Field(k => k.ActivityId,"activityId")
                .Field(p=> p.ProjectId, "projectId")
                .Field(p=> p.Description,"description")
                .Field(p=> p.Status, "status")
                ;

            #endregion
        }
    }
}