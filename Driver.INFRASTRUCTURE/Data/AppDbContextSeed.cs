using Driver.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Driver.INFRASTRUCTURE.Data
{
    public class AppDbContextSeed
    {

        public static async Task SeedAsync(AppDbContext context, ILoggerFactory loggerFactory, int? retry = 0)
        {
            var retryForAvailability = retry;

            try
            {
                await context.Database.MigrateAsync();

                if (!context.Companies.Any())
                {
                    await context.Companies.AddRangeAsync(GetCompanies());
                    await context.SaveChangesAsync();
                }

                if (!context.Drivers.Any())
                {
                    await context.Drivers.AddRangeAsync(GetDrivers());
                    await context.SaveChangesAsync();
                }

                if (!context.Assessments.Any())
                {
                    await context.Assessments.AddRangeAsync(GetAssessments());
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<AppDbContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(context, loggerFactory, retryForAvailability);
                }

            }
        }

        private static IEnumerable<Company> GetCompanies()
        {
            var companies =
                new List<Company>
                {
                    new Company { Id = new Guid("19993E28-3478-4FC3-AF63-D226BD280D4B"), Name = "T-Mobile" },
                    new Company { Id = new Guid("8415BE5A-933B-4D09-9FC5-ABA57F3B85B3"), Name = "Sky" },
                };

            return companies;
        }

        private static IEnumerable<CORE.Entities.Driver> GetDrivers()
        {
            var drivers =
                new List<CORE.Entities.Driver>
                {
                    new CORE.Entities.Driver { Id = new Guid("2C3912FD-2731-4FEC-A29F-0AC9AA21673F"), Name = "James Hunt",  CompanyId = new Guid("19993E28-3478-4FC3-AF63-D226BD280D4B"), HasUKLicence=true, DateOfBirth = new DateTime(1960, 1, 1) },
                    new CORE.Entities.Driver { Id = new Guid("C2D09D4F-FED7-4D4D-9882-977E0860A5F8"), Name = "Jenson Button",  CompanyId = new Guid("19993E28-3478-4FC3-AF63-D226BD280D4B"), HasUKLicence=true, DateOfBirth = new DateTime(1980, 1, 1) },
                    new CORE.Entities.Driver { Id = new Guid("7F94C582-FEC1-43A1-9570-9C10BD98DC2A"), Name = "Lewis Hamilton",  CompanyId = new Guid("19993E28-3478-4FC3-AF63-D226BD280D4B"), HasUKLicence=true, DateOfBirth = new DateTime(1985, 1, 1) },
                    new CORE.Entities.Driver { Id = new Guid("8131C35C-32D6-4D79-88EA-1FD7264A7B1B"), Name = "Michael Schumacher",  CompanyId = new Guid("19993E28-3478-4FC3-AF63-D226BD280D4B"), HasUKLicence=false, DateOfBirth = new DateTime(1990, 1, 1) },
                    new CORE.Entities.Driver { Id = new Guid("EEBF1E7A-031F-4836-AE1A-CBE4773F95FA"), Name = "Damon Hill",  CompanyId = new Guid("8415BE5A-933B-4D09-9FC5-ABA57F3B85B3"), HasUKLicence=true, DateOfBirth = new DateTime(2002, 1, 1) }
                };

            return drivers;
        }


        private static IEnumerable<Assessment> GetAssessments()
        {
            var assessments =
                new List<Assessment>
                {
                    new Assessment { Id = new Guid("93DCE181-0D2F-4AAB-A0F5-204FF96D09C7"), TestDate = new DateTime(2019, 12, 3), Examiner="Steve Smith", PassedDriving=true, PassedTheory=true, PassedEyeExam=true, DriverId=new Guid("2C3912FD-2731-4FEC-A29F-0AC9AA21673F") },
                    new Assessment { Id = new Guid("6CD03868-E4EA-488B-B1C8-8278A8F55016"), TestDate = new DateTime(2019, 8, 8), Examiner="Steve Smith", PassedDriving=true, PassedTheory=true, PassedEyeExam=false, DriverId=new Guid("C2D09D4F-FED7-4D4D-9882-977E0860A5F8") },
                    new Assessment { Id = new Guid("945D5BF2-03F7-4E9C-9009-043A454E2D6D"), TestDate = new DateTime(2005, 1, 1), Examiner = "Bill Bryson", PassedDriving = true, PassedTheory = true, PassedEyeExam = true, DriverId = new Guid("7F94C582-FEC1-43A1-9570-9C10BD98DC2A") },
                    new Assessment { Id = new Guid("DE87C05A-70B2-47F8-BA90-150ABA89BE7F"), TestDate = new DateTime(2005, 1, 1), Examiner = "Andy Anderson", PassedDriving = true, PassedTheory = true, PassedEyeExam = true, DriverId = new Guid("8131C35C-32D6-4D79-88EA-1FD7264A7B1B") },
                    new Assessment { Id = new Guid("57AE4323-8A62-4CC3-BE61-DE0FF810BEB5"), TestDate = new DateTime(2001, 1, 1), Examiner = "Andy Anderson", PassedDriving = true, PassedTheory = true, PassedEyeExam = true, DriverId = new Guid("EEBF1E7A-031F-4836-AE1A-CBE4773F95FA") }
                };

            return assessments;
        }
    }
}
