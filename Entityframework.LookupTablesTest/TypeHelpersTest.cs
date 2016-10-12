using EntityFramework.LookupTables;
using ExampleUsage.Models;
using Xunit;

namespace Entityframework.LookupTablesTest
{
    public class TypeHelpersTest
    {
        [Fact]
        [Trait("Category", "CI")]
        public void GetEntitiesThatImplementILookupTable()
        {
            //arrange
            //act
            var result = TypeHelpers.GetLookupTableTypes();
            //assert
            Assert.NotEmpty(result);
        }

        [Fact]
        [Trait("Category", "CI")]
        public void CallDoesTypeSupportInterface()
        {
            //arrange
            var testType = typeof(Test);
            var lookUpType = typeof(ILookupTable<>);
            //act
            var result = TypeHelpers.DoesTypeSupportInterface(testType, lookUpType);
            //assert
            Assert.True(result);
        }
    }
}