using EntityFramework.LookupTables;

namespace ExampleUsage
{
    public abstract class BaseEntity : IIdentity<int>
    {
        public int Id { get; set; }
    }
}