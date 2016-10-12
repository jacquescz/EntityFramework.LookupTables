using EntityFramework.LookupTables;
using ExampleUsage.LookupTables;

namespace ExampleUsage.Models
{
    public class Test : BaseEntity, ILookupTable<int>
    {
        public static implicit operator Test(TestEnum testEnum) => new Test(testEnum);

        public static implicit operator TestEnum(Test test) => (TestEnum)test.Id;

        private Test(TestEnum testEnum)
        {
            Id = (int)testEnum;
            Description = testEnum.ToString();
        }

        protected Test()
        {
        }

        public string Description { get; set; }
    }
}