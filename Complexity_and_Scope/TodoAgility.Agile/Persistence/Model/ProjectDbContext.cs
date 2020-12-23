using Microsoft.EntityFrameworkCore;
using TodoAgility.Agile.Persistence.Framework.Model;

namespace TodoAgility.Agile.Persistence.Model
{
    public class ProjectDbContext : AggregateDbContext
    {
        public ProjectDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ProjectState> Projects { get; set; }
        public DbSet<ActivityStateReference> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region ConfigureProject

            modelBuilder.Entity<ProjectState>(
                b =>
                {
                    b.Property(e => e.ProjectId).ValueGeneratedNever().IsRequired();
                    b.HasKey(e => e.ProjectId);
                    b.Property(e => e.Description).IsRequired();
                    b.Property(p => p.PersistenceId);
                    b.HasQueryFilter(q => !q.IsDeleted);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();
                });

            modelBuilder.Entity<ActivityStateReference>(
                b =>
                {
                    b.Property(k => k.ActivityReferenceId)
                        .ValueGeneratedNever().IsRequired();
                    b.HasKey(k => k.ActivityReferenceId);
                    b.HasOne<ProjectState>()
                        .WithMany(m => m.Activities)
                        .HasForeignKey(f => f.ProjectId);

                    b.Property(p => p.PersistenceId);
                    b.HasQueryFilter(q => !q.IsDeleted);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();
                    b.HasIndex(idx => new {idx.ActivityReferenceId, idx.ProjectId}).IsUnique();
                });

            #endregion
        }
    }
}