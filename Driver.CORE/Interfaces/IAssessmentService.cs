using Driver.CORE.Entities;
using System;
using System.Collections.Generic;

namespace Driver.CORE.Interfaces
{
    public interface IAssessmentService
    {
        Assessment GetById(Guid id);

        List<Assessment> GetAll();

        List<Assessment> GetAllPassed();

        List<Assessment> GetAllPassedForCompany(Guid companyId);

        bool IsPassed(Assessment assessment);
    }
}
