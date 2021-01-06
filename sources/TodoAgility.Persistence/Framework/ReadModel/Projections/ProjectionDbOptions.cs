using LiteDB;

namespace TodoAgility.Persistence.Framework.ReadModel.Projections
{
    public class ProjectionDbOptions
    {
        public const string ProjectionDb = "ProjectionDb";

        public string ConnectionString { get; set; }
    }
}