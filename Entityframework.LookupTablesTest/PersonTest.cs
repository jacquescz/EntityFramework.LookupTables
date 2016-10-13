using ExampleUsage;
using ExampleUsage.LookupTables;
using ExampleUsage.Models;
using System.Linq;
using Xunit;

namespace Entityframework.LookupTablesTest
{
    public class PersonTest
    {
        private MyContext Context;

        public PersonTest()
        {
            Context = new MyContext();
        }

        [Fact]
        [Trait("Category", "CI")]
        public void Exists()
        {
            //arrange
            Person p;
            //act
            p = new Person();
            //assert
            Assert.NotNull(p);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GetPersonByEnum()
        {
            //arrange
            var expected = (int)TestEnum.value1;
            //act
            var result = Context.Set<Person>().FirstOrDefault(x => x.TestId == expected);
            //assert
            Assert.Equal(expected, result.TestId);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GetEnum()
        {
            //arrange
            var expected = (int)TestEnum.value1;
            //act
            var result = Context.Set<Test>().FirstOrDefault();
            //assert
            Assert.Equal(expected, result.Id);
        }

        [Fact]
        [Trait("Category", "Integration")]
        public void GetPersonByEnumMakeSureTestExists()
        {
            //arrange
            var expected = (int)TestEnum.value1;
            //act
            var result = Context.Set<Person>().FirstOrDefault(x => x.TestId == expected);
            //assert
            Assert.NotNull(result.Test);
            Assert.Equal(expected, result.Test.Id);
        }
    }
}