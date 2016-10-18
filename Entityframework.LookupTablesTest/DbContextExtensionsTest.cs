using EntityFramework.LookupTables;
using ExampleUsage;
using ExampleUsage.LookupTables;
using ExampleUsage.Models;
using System;
using System.Linq;
using Xunit;

namespace EntityFramework.LookupTablesTest
{
    public class DbContextExtensionsTest
    {
        [Fact]
        [Trait("Category", "Integration")]
        public void CallGenericSeedEnumsValuesWithGivenTypes()
        {
            //arrange
            var context = new MyContext();
            //act
            var action = new Action(() => { context.SeedEnumValues(typeof(string), typeof(object)); });
            //assert
            Assert.ThrowsAny<Exception>(action);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void SeedAllIDescriptionEnumsEntities()
        {
            //arrange
            var context = new MyContext();
            var set = context.Set<Test>();
            context.DeleteIgnorePrimaryKey<Test, TestEnum>();
            //act
            context.SeedAllEnumValues();
            var result = set.FirstOrDefault();
            //assert
            Assert.Equal(TestEnum.value1.ToString(), result.Description);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void CallGenericSeedEnumsValuesWithValidGivenTypes()
        {
            //arrange
            var context = new MyContext();
            var set = context.Set<Test>();
            context.DeleteIgnorePrimaryKey<Test, TestEnum>();
            //act
            context.SeedEnumValues(typeof(Test), typeof(TestEnum));
            var result = set.FirstOrDefault();
            //assert
            Assert.Equal(TestEnum.value1.ToString(), result.Description);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void CallGenericSeedEnumsValuesWithValidGivenTypes2()
        {
            //arrange
            var context = new MyContext();
            var set = context.Set<Test>();
            context.DeleteIgnorePrimaryKey<Test, TestEnum>();
            System.Threading.Thread.Sleep(1000);
            //act
            context.SeedEnumValues<Test, TestEnum>();
            var result = set.FirstOrDefault();
            //assert
            Assert.Equal(TestEnum.value1.ToString(), result.Description);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void CallDeleteIgnorePrimaryKey()
        {
            //arrange
            var context = new MyContext();
            var persons = context.Set<Person>();
            context.DeleteRangeIgnorePrimaryKey<Person, Test, TestEnum>(persons);
            var test = context.Set<Test>();
            context.SeedEnumValues<Test, TestEnum>();
            //act
            context.DeleteIgnorePrimaryKey(test.FirstOrDefault());
            var result = test.ToList();
            //assert
            Assert.True(result.Count() == 1);
            context.SeedEnumValues<Test, TestEnum>();
        }
    }
}