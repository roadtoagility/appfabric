using LiteDB;

namespace TodoAgility.Persistence.Framework.Projections
{
    public abstract class ProjectionDbContext
    {
        protected ProjectionDbContext(string connectionString, BsonMapper modelBuilder)
        {
            Database = new LiteDatabase(connectionString);
        }

        public ILiteDatabase Database { get; }

    }
}