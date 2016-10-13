using ExampleUsage.LookupTables;
using ExampleUsage.Models;

namespace ExampleUsage
{
    public class PersonData
    {
        public Person Create() => new Person { Id = 1, Name = "TestPerson", TestId = (int)TestEnum.value1 };
    }
}