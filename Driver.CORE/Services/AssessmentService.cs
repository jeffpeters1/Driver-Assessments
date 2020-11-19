using Driver.CORE.Entities;
using Driver.CORE.Interfaces;
using Driver.CORE.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Driver.CORE.Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAsyncRepository<Assessment> repository;

        public AssessmentService(IAsyncRepository<Assessment> repository)
        {
            this.repository = repository;
        }

        public Assessment GetById(Guid id)
        {
            var spec = new AssessmentSpecification(id);
            return repository.GetSingleBySpec(spec);
        }

        public List<Assessment> GetAll()
        {
            var spec = new AssessmentSpecification(x => x != null);

            return repository.List(spec)
                             .OrderBy(x => x.TestDate)
                             .ToList();
        }

        public List<Assessment> GetAllPassed()
        {
            var assessments = GetAll();
            var passed = new List<Assessment>();

            foreach(var assessment in assessments)
            {
                if (IsPassed(assessment))
                    passed.Add(assessment);
            }

            return passed;
        }

        public List<Assessment> GetAllPassedForCompany(Guid companyId)
        {
            return GetAllPassed().Where(x => x.Driver.CompanyId == companyId)
                                 .ToList();
        }

        public bool IsPassed(Assessment assessment)
        {
            return assessment.PassedDriving == true &&
                   assessment.PassedTheory == true &&
                   assessment.PassedEyeExam == true;                                            
        }




        //&& IsOver25(assessment);  
        private bool IsOver25(Assessment assessment)
        {
            var today = DateTime.Today;
            var yearBorn = assessment.Driver.DateOfBirth.Year;

            var age = today.Year - yearBorn;

            return age > 25;
        }
    }

}
