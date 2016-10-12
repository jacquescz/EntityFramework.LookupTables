using EntityFramework.LookupTables;
using ExampleUsage;
using ExampleUsage.LookupTables;
using ExampleUsage.Models;
using System.Linq;
using Xunit;

namespace Entityframework.LookupTablesTest
{
    public class DbSetExtensionsTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void CallGenericSeedEnumsValuesFromDbSetWithValidGivenTypes()
        {
            //arrange
            var context = new MyContext();
            context.DeleteIgnorePrimaryKey<Test, TestEnum>();
            //act
            context.Set<Test>().SeedEnumValues<Test, TestEnum>();
            context.SaveChanges();
            var result = context.Set<Test>().FirstOrDefault();
            //assert
            Assert.Equal(TestEnum.value1.ToString(), result.Description);
        }
    }
}