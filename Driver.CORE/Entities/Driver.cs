using System;
using System.Collections.Generic;

namespace Driver.CORE.Entities
{
    public class Driver : BaseEntity
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool HasUKLicence { get; set; }


        // Relationships
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Assessment> Assessments { get; set; }
    }
}
