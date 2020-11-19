using System;
using System.Collections.Generic;
using System.Text;

namespace Driver.CORE.Helpers
{
    public static class ExtensionMethods
    {
        public static int GetAge(this DateTime birthdate)
        {
            var today = DateTime.Today;

            var age = today.Year - birthdate.Year;

            if (birthdate.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
