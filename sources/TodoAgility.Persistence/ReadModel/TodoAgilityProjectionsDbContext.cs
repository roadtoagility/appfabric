using LiteDB;
using Microsoft.EntityFrameworkCore;
using TodoAgility.Persistence.Framework.Model;

namespace TodoAgility.Persistence.ReadModel
{
    public sealed class TodoAgilityProjectionsDbContext : AggregateDbContext
    {
        public TodoAgilityProjectionsDbContext(DbContextOptions<TodoAgilityProjectionsDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<ProjectProjection> Projects { get; set; }
        
        public DbSet<UserProjection> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region ConfigureView

            modelBuilder.Entity<ProjectProjection>(p =>
            {
                p.Property(pr => pr.Id).ValueGeneratedNever();
                p.HasKey(pr => pr.Id);
                p.Property(pr => pr.Name);
                p.Property(pr => pr.Code);
                p.Property(pr => pr.StartDate);
                p.Property(pr => pr.Budget);
                p.Property(pr => pr.ClientId);
            });
            
            modelBuilder.Entity<UserProjection>(u =>
            {
                u.Property(pr => pr.Id).ValueGeneratedNever();
                u.HasKey(pr => pr.Id);
                u.Property(pr => pr.Name);
                u.Property(pr => pr.Cnpj);
                u.Property(pr => pr.CommercialEmail);
            });

            #endregion
        }
    }
}