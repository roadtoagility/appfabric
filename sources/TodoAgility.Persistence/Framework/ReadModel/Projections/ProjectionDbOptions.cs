using LiteDB;

namespace TodoAgility.Persistence.Framework.ReadModel.Projections
{
    public class ProjectionDbOptions
    {
        public const string ProjectionConnectionStrings = "ProjectionConnectionStrings";

        public string DefaultConnection { get; set; }
    }
}