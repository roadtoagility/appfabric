using LiteDB;
using Microsoft.Extensions.Options;

namespace TodoAgility.Persistence.Framework.ReadModel.Projections
{
    public abstract class ProjectionDbContext
    {
        protected ProjectionDbContext(IOptions<ProjectionDbOptions> options)
        {
            Database = new LiteDatabase(options.Value.ConnectionString);
        }

        public ILiteDatabase Database { get; }
    }
}