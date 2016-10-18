using ExampleUsage.Models;
using System.Data.Entity.Migrations;

namespace ExampleUsage
{
    public class DatabaseInitializer : EntityFramework.LookupTables.DropCreateDatabaseAlways<MyContext>
    {
 
        protected override void Seed(MyContext context)
        {
            var person = new PersonData().Create();
            context.Set<Person>().AddOrUpdate(person);
            context.SaveChanges();
        }
    }
}