using Driver.CORE.Entities;
using Driver.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Driver.WEB.Interfaces
{
    public interface IIndexService
    {
        IndexVm GetAllPassed(Guid companyId);

        List<SelectListItem> GetAllCompanies();
    }
}
