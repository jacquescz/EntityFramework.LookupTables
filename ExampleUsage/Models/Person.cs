namespace ExampleUsage.Models
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}