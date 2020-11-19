using System;

namespace Driver.CORE.Entities
{
    public class Assessment : BaseEntity
    {
        public DateTime TestDate { get; set; }

        public string Examiner { get; set; }

        //---------

        public bool PassedDriving { get; set; }

        public bool PassedTheory { get; set; }

        public bool PassedEyeExam { get; set; }

        //---------

        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }


        public override string ToString()
        {
            return PassedDriving && PassedTheory && PassedEyeExam ? "Pass" : "Fail";
        }

    }
}
