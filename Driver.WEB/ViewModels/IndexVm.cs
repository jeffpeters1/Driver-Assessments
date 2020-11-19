using Driver.CORE.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Driver.WEB.ViewModels
{
    public class IndexVm
    {
        [Required]
        public Guid CompanyId { get; set; }

        public string CompanyName { get; set; }

        public List<AssessmentVm> Assessments { get; set; }
    }
}
