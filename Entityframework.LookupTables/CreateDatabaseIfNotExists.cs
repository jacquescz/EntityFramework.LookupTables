using EntityFramework.LookupTables;
using System.Data.Entity;
using System.Diagnostics.Contracts;

namespace Entityframework.LookupTables
{
    public class CreateDatabaseIfNotExists<TDbContext> : IDatabaseInitializer<TDbContext>
        where TDbContext : DbContext
    {
        public void InitializeDatabase(TDbContext context)
        {
            Contract.Requires(context != null, nameof(context));

            var exists = context.Database.Exists();

            if (exists && context.Database.CompatibleWithModel(true)) return;

            if (!exists)
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