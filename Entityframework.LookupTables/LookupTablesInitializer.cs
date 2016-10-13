using System.Data.Entity;

namespace Entityframework.LookupTables
{
    public class LookupTablesInitializer<TDbContext> : CreateDatabaseIfNotExists<TDbContext>
        where TDbContext : DbContext
    {
    }
}