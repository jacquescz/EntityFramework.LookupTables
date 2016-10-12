using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EntityFramework.LookupTables;
using ExampleUsage.Models;
using System.Reflection;

namespace ExampleUsage
{
    public sealed class MyContext : DbContext
    {
        public MyContext() : base("test")
        {
            Database.SetInitializer<MyContext>(new DropCreateDatabaseAlways<MyContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add<LookupTableConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(GetType()));
            base.OnModelCreating(modelBuilder);
        }
    }
}
