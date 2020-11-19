using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Driver.WEB.ViewModels
{
    public class AssessmentVm
    {
        public DateTime TestDate { get; set; }

        public string Examiner { get; set; }

        public string CompanyName { get; set; }

        public string DriverName { get; set; }

        public int DriverAge { get; set; }

        public string PassedDriving { get; set; }

        public string PassedTheory { get; set; }

        public string PassedEyeExam { get; set; }

        public string Verdict { get; set; }
    }
}
