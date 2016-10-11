using System.Data.Entity.ModelConfiguration.Conventions;

namespace EntityFramework.LookupTables
{
    public sealed class LookupTableConvention : Convention
    {
        public LookupTableConvention()
        {
            this.LookupTable<long>();
            this.LookupTable<int>();
        }
    }
}