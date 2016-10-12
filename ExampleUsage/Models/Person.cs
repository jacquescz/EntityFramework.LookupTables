using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleUsage.Models
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
