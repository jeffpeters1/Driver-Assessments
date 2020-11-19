using Driver.CORE.Entities;
using Driver.CORE.Interfaces;
using Driver.CORE.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Driver.CORE.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IAsyncRepository<Company> repository;

        public CompanyService(IAsyncRepository<Company> repository)
        {
            this.repository = repository;
        }

        public List<Company> GetAll()
        {
            return repository.ListAll()
                             .OrderBy(x => x.Name)
                             .ToList();
        }

        public Company GetById(Guid id)
        {
            var spec = new CompanySpecification(id);
            return repository.GetSingleBySpec(spec);
        }
    }
}
