using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KokaarWebApiGabarit.API.ActionFilters;
using KokaarWebApiGabarit.Infrastructure.Contracts;
using KokaarWebApiGabarit.Model.DataTransferObjects;
using KokaarWebApiGabarit.Model.Entities;
using KokaarWebApiGabarit.Model.RequestFeatures;
using KokaarWebApiGabarit.Persistance.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KokaarWebApiGabarit.API.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<EmployeeDto> _dataShaper;

        public EmployeesController(IRepositoryManager repository, ILoggerService logger, 
            IMapper mapper, IDataShaper<EmployeeDto> dataShaper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
        }

        [HttpGet]
        [HttpHead]
        public async Task<IActionResult> GetEmployeesForCompany(int companyId, 
            [FromQuery] EmployeeParameters employeeParameters)
        {
            if (!employeeParameters.ValidAgeRange) 
                return BadRequest("Max age can't be less than min age.");

            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var employeesFromDb = await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(employeesFromDb.MetaData));

            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return Ok(employeesDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeForCompany(int companyId, int id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var employeeDb = _repository.Employee.GetEmployee(companyId, id, trackChanges: false);
            if (employeeDb == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var employee = _mapper.Map<EmployeeDto>(employeeDb);
            return Ok(employee);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateEmployeeForCompany(int companyId, [FromBody] EmployeeForCreationDto employee)
        {            
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var employeeEntity = _mapper.Map<Employee>(employee);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _repository.Save();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
        public IActionResult DeleteEmployeeForCompany(int companyId, int id)
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var employeeForCompany = _repository.Employee.GetEmployee(companyId, id, trackChanges: false);
            if (employeeForCompany == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Employee.DeleteEmployee(employeeForCompany);
            _repository.Save();
            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
        public IActionResult UpdateEmployeeForCompany(int companyId, int id,
            [FromBody] EmployeeForUpdateDto employee)
        {            
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            var employeeEntity = _repository.Employee.GetEmployee(companyId, id, trackChanges: true);
            if (employeeEntity == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(employee, employeeEntity);
            _repository.Save();
            return NoContent();
        }

        //[HttpPatch("{id}")]
        //[ServiceFilter(typeof(ValidateEmployeeForCompanyExistsAttribute))]
        //public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(int companyId, int id, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
        //{
        //    if (patchDoc == null)
        //    {
        //        _logger.LogError("patchDoc object sent from client is null.");
        //        return BadRequest("patchDoc object is null");
        //    }
        //    var employeeEntity = HttpContext.Items["employee"] as Employee;
        //    var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
        //    patchDoc.ApplyTo(employeeToPatch, ModelState);
        //    TryValidateModel(employeeToPatch);
        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogError("Invalid model state for the patch document");
        //        return UnprocessableEntity(ModelState);
        //    }
        //    _mapper.Map(employeeToPatch, employeeEntity);
        //    await _repository.SaveAsync();
        //    return NoContent();
        //}

    }
}