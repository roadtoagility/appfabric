using LiteDB;
using Microsoft.Extensions.Options;

namespace AppFabric.Persistence.Framework.ReadModel.Projections
{
    public abstract class ProjectionDbContext
    {
        protected ProjectionDbContext(IOptions<ProjectionDbOptions> options)
        {
            Database = new LiteDatabase(options.Value.DefaultConnection);
        }

        public ILiteDatabase Database { get; }
    }
}