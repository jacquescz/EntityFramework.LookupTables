using EntityFramework.LookupTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleUsage
{
    public abstract class BaseEntity : IIdentity<int>
    {
        public int Id { get; set; }
    }
}
