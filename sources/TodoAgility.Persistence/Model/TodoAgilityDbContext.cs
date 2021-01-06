using Microsoft.EntityFrameworkCore;
using TodoAgility.Persistence.Framework.Model;

namespace TodoAgility.Persistence.Model
{
    public class TodoAgilityDbContext : AggregateDbContext
    {
        public TodoAgilityDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ProjectState> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ConfigureProject

            modelBuilder.Entity<ProjectState>(
                b =>
                {
                    b.Property(e => e.Id).ValueGeneratedNever().IsRequired();
                    b.Property(e => e.Code).IsRequired();
                    b.Property(e => e.Name).IsRequired();
                    b.Property(e => e.Budget).IsRequired();
                    b.HasKey(e => e.Id);
                    b.Property(e => e.ClientId).IsRequired();
                    b.Property(e => e.StartDate).IsRequired();
                    b.Property(p => p.PersistenceId);
                    b.HasQueryFilter(q => !q.IsDeleted);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();
                });

            #endregion
        }
    }
}