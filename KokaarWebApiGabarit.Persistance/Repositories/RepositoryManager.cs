﻿using KokaarWebApiGabarit.Persistance.Contracts;
using KokaarWebApiGabarit.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KokaarWebApiGabarit.Persistance.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IEmployeeRepository _employeeRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);
                return _companyRepository;
            }
        }
        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);
                return _employeeRepository;
            }
        }
        public void Save() => _repositoryContext.SaveChanges();

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }

}
