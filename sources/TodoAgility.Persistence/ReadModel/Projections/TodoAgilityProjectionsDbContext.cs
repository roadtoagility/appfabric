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
            Users = Database.GetCollection<UserProjection>("user");
        }
        
        public ILiteCollection<ActivityProjection> Activities { get; }
        
        public ILiteCollection<ProjectProjection> Projects { get; }
        
        public ILiteCollection<UserProjection> Users { get; }
        
        private void OnModelCreating(BsonMapper modelBuilder)
        {
            #region ConfigureView
            
            modelBuilder.Entity<ProjectProjection>()
                .Field(p=> p.Id, "projectId")
                .Field(p=> p.Name, "name")
                .Field(p=> p.Code, "code")
                .Field(p=> p.StartDate, "startDate")
                .Field(p=> p.Budget, "budget")
                .Field(p=> p.ClientId, "clientId")
                ;
            
            modelBuilder.Entity<UserProjection>()
                .Field(p=> p.Id, "projectId")
                .Field(p=> p.Name, "name")
                .Field(p=> p.Cnpj, "cnpj")
                .Field(p=> p.CommercialEmail, "commercialEmail")
                ;
            #endregion
        }
    }
}