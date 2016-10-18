using System.Data.Entity;

namespace EntityFramework.LookupTables
{
    public class LookupTablesInitializer<TDbContext> : CreateDatabaseIfNotExists<TDbContext>
        where TDbContext : DbContext
    {
    }
}