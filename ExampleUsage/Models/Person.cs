namespace ExampleUsage.Models
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public int TestId { get; set; }
        public virtual Test Test { get; set; }
    }
}