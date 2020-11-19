using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driver.CORE.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Inserted { get; set; } = DateTime.UtcNow;
    }
}
