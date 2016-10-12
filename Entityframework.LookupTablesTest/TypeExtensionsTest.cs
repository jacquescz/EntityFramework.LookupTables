using EntityFramework.LookupTables;
using ExampleUsage.LookupTables;
using ExampleUsage.Models;
using System;
using System.Linq;
using Xunit;

namespace Entityframework.LookupTablesTest
{
    public class TypeExtensionsTest
    {
        [Fact]
        [Trait("Category", "CI")]
        public void GivenValidTypeGetEnumValuesFor()
        {
            //arrange
            var testType = typeof(TestEnum);

            //act
            var result = testType.GetEnumValuesFor();
            //assert
            Assert.Equal(TestEnum.value1, result.First());
        }

        [Fact]
        [Trait("Category", "CI")]
        public void GivenValidTypeGetAssemblyEnumsFor()
        {
            //arrange
            var testType = typeof(TestEnum);

            //act
            var result = testType.GetAssemblyEnumsFor();
            //assert
            Assert.Equal(testType, result);
        }

        [Fact]
        [Trait("Category", "CI")]
        public void GivenTypeCheckTypeForValidEnum()
        {
            //arrange
            var testType = typeof(Test);
            //act
            var action = new Action(() => { testType.EnumTypeCheck(); });
            //assert
            Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        [Trait("Category", "CI")]
        public void GivenTypeCheckTypeForValidEnum2()
        {
            //arrange
            //act
            var action = new Action(() => { TypeExtensions.EnumTypeCheck<Test>(); });
            //assert
            Assert.Throws<ArgumentException>(action);
        }
    }
}