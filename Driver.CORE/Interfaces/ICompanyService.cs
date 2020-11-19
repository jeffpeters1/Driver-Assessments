using Driver.CORE.Entities;
using System;
using System.Collections.Generic;

namespace Driver.CORE.Interfaces
{
    public interface ICompanyService
    {
        List<Company> GetAll();

        Company GetById(Guid id);
    }
}
