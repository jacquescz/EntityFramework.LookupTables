using EntityFramework.LookupTables;
using System.Data.Entity;
using System.Diagnostics.Contracts;

namespace Entityframework.LookupTables
{
    public class DropCreateDatabaseAlways<TDbContext> : IDatabaseInitializer<TDbContext>
        where TDbContext : DbContext
    {
        public void InitializeDatabase(TDbContext context)
        {
            Contract.Requires(context != null, nameof(context));

            context.Database.Delete();

            if (!context.Database.Exists())
            {
                context.Database.Create();
            }

            context.SeedAllEnumValues();
            Seed(context);
        }

        protected virtual void Seed(TDbContext context)
        {
        }
    }
}