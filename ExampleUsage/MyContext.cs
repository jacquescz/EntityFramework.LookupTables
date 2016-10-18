using EntityFramework.LookupTables;
using System.Data.Entity;
using System.Reflection;

namespace ExampleUsage
{
    public sealed class MyContext : DbContext
    {
        public MyContext() : base("test")
        {
        }

        static MyContext()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ////Add conventions.
            modelBuilder.Conventions.Add<LookupTableConvention>();
            ////Registers to the modelBuilder all entities that inherits from EntityTypeConfiguration<>
            ////this in not mandatory for the lookuptables to work.
            var assambly = Assembly.GetAssembly(GetType());
            modelBuilder.Configurations.AddFromAssembly(assambly);
            base.OnModelCreating(modelBuilder);
        }
    }
}