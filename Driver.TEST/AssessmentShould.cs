using Driver.CORE.Entities;
using Driver.CORE.Services;
using Driver.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace Driver.TEST
{
    [TestFixture]
    public class AssessmentShould
    {
        private const string cnnString = "Server=.\\SQLEXPRESS;Database=Driver;Trusted_Connection=True;MultipleActiveResultSets=true";
        private DbContextOptionsBuilder builder;
        private AppDbContext context;
        private Repository<Assessment> repository;
        private AssessmentService service;

        [SetUp]
        public void SetUp()
        {
            builder = new DbContextOptionsBuilder().UseSqlServer(cnnString);
            context = new AppDbContext(builder.Options);
            repository = new Repository<Assessment>(context);
            service = new AssessmentService(repository);
        }

        [Test]
        public void Pass_EyeExam_ShouldBeTrue()
        {
            var driver_20yo = new CORE.Entities.Driver()
            {
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            var assessment = new Assessment()
            {
                Driver = driver_20yo,
                PassedDriving = true,
                PassedTheory = true,
                PassedEyeExam = true
            };

            Assert.That(service.IsPassed(assessment), Is.True);
        }

        [Test]
        public void Fail_EyeExam_ShouldBeFalse()
        {
            var driver_20yo = new CORE.Entities.Driver()
            {
                DateOfBirth = new DateTime(2000, 1, 1)
            };

            var assessment = new Assessment()
            {
                Driver = driver_20yo,
                PassedDriving = true,
                PassedTheory = true,
                PassedEyeExam = false
            };

            Assert.That(service.IsPassed(assessment), Is.False);
        }
    }
}
