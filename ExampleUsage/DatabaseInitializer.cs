using ExampleUsage.Models;
using System.Data.Entity.Migrations;

namespace ExampleUsage
{
    public class DatabaseInitializer : Entityframework.LookupTables.DropCreateDatabaseAlways<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            var person = new PersonData().Create();
            context.Set<Person>().AddOrUpdate(person);
            context.SaveChanges();
            base.Seed(context);
        }
    }
}