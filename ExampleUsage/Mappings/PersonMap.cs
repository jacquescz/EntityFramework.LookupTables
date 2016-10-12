using ExampleUsage.Models;
using System.Data.Entity.ModelConfiguration;

namespace ExampleUsage.Mappings
{
    public sealed class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            HasRequired(x => x.Test)
             .WithMany()
             .HasForeignKey(x => x.TestId)
             .WillCascadeOnDelete(false);
        }
    }
}