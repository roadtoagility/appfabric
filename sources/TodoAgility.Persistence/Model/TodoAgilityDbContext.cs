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
        public DbSet<UserState> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    b.Property(q => q.IsDeleted);
                    b.HasQueryFilter(project => EF.Property<bool>(project, "IsDeleted") == false);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();
                });

            modelBuilder.Entity<UserState>(
                b =>
                {
                    b.Property(e => e.Id).ValueGeneratedNever().IsRequired();
                    b.Property(e => e.Name).IsRequired();
                    b.Property(e => e.Cnpj).IsRequired();
                    
                    b.Property(p => p.PersistenceId);
                    b.Property(e => e.IsDeleted);
                    b.HasQueryFilter(user => EF.Property<bool>(user, "IsDeleted") == false);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();
                });
            
            modelBuilder.Entity<ClientState>(
                b =>
                {
                    b.Property(e => e.ProjectId).ValueGeneratedNever();
                    b.Property(e => e.ClientId).IsRequired();
                    b.HasKey(e => e.ClientId);
                    b.Property(p => p.PersistenceId);
                    b.Property(q => q.IsDeleted);
                    b.HasQueryFilter(client => EF.Property<bool>(client, "IsDeleted") == false);
                    b.Property(e => e.CreateAt);
                    b.Property(e => e.RowVersion).ValueGeneratedOnAddOrUpdate().IsRowVersion();
                });

        }
    }
}