using Driver.CORE.Entities;
using Driver.CORE.Helpers;
using Driver.CORE.Interfaces;
using Driver.WEB.Interfaces;
using Driver.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Driver.WEB.Services
{
    public class IndexService : IIndexService
    {
        private readonly IAssessmentService assessmentService;
        private readonly ICompanyService companyService;

        public IndexService(IAssessmentService assessmentService, ICompanyService companyService)
        {
            this.assessmentService = assessmentService;
            this.companyService = companyService;
        }

        public List<SelectListItem> GetAllCompanies()
        {
            return companyService.GetAll()
                                 .Select(c => new SelectListItem() { Text = c.Name, Value = c.Id.ToString() })
                                 .ToList();
        }

        public IndexVm GetAllPassed(Guid companyId)
        {
            var id = Guid.Empty;
            var name = "All Companies";

            // Get Passed Assessments
            var list = Map(companyId == Guid.Empty ? assessmentService.GetAllPassed() :
                                                 assessmentService.GetAllPassedForCompany(companyId));

            // Get company details
            var company = companyService.GetById(companyId);
            
            // Populate for company
            if (company != null)
            {
                id = company.Id;
                name = company.Name;   
            }

            return new IndexVm()
            {
                Assessments = list,
                CompanyId = id,
                CompanyName = name
            };
        }

        private List<AssessmentVm> Map(List<Assessment> assessments)
        {
            var list = new List<AssessmentVm>();

            foreach (var assessment in assessments)
            {
                list.Add(new AssessmentVm()
                {
                    CompanyName = assessment.Driver.Company.Name,
                    DriverName = assessment.Driver.Name,
                    DriverAge = (assessment.Driver.DateOfBirth).GetAge(), 
                    Examiner = assessment.Examiner,
                    PassedDriving = assessment.PassedDriving == true ? Icons.Tick : Icons.Cross,
                    PassedEyeExam = assessment.PassedEyeExam == true ? Icons.Tick : Icons.Cross,
                    PassedTheory = assessment.PassedTheory == true ? Icons.Tick : Icons.Cross,
                    TestDate = assessment.TestDate,
                    Verdict = assessment.ToString()
                });   
            }

            return list;
        }
    }
}
