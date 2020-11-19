using System.Collections.Generic;

namespace Driver.CORE.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Driver> Drivers { get; set; }
    }
}
