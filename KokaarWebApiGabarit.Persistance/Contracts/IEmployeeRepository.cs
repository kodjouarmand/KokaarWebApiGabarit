using KokaarWebApiGabarit.Model.Entities;
using KokaarWebApiGabarit.Model.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KokaarWebApiGabarit.Persistance.Contracts
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees(int companyId, bool trackChanges);
        Employee GetEmployee(int companyId, int id, bool trackChanges);
        void CreateEmployeeForCompany(int companyId, Employee employee);
        void DeleteEmployee(Employee employee);

        Task<IEnumerable<Employee>> GetAllEmployeesAsync(int companyId, bool trackChanges);
        Task<Employee> GetEmployeeAsync(int companyId, int id, bool trackChanges);
        Task<PagedList<Employee>> GetEmployeesAsync(int companyId, EmployeeParameters employeeParameters, bool trackChanges);
    }
}
