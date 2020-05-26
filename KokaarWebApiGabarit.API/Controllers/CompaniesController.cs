using Microsoft.AspNetCore.Mvc;
using KokaarWebApiGabarit.Model.DataTransferObjects;
using KokaarWebApiGabarit.API.ActionFilters;
using Marvin.Cache.Headers;
using KokaarWebApiGabarit.Infrastructure.Contracts;
using KokaarWepApi.Business.Contracts;
using KokaarWebApiGabarit.Business.Enums;

namespace KokaarWebApiGabarit.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILoggerService _logger;
        public CompaniesController(ILoggerService logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        //[HttpGet(Name = "GetCompanies"), Authorize(Roles = "Manager")]
        [HttpGet(Name = "GetCompanies")]
        public  IActionResult GetAll()
        {
            var companies =  _companyService.GetAll();
            return Ok(companies);
        }

        [HttpGet("{companyId}", Name = "GetCompanyById")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public  IActionResult GetById(int companyId)
        {
            var company =  _companyService.GetById(companyId);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(company);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public  IActionResult Add([FromBody]CompanyDto companyToAdd)
        {
            _companyService.CurrentUser = "armand";
             _companyService.Add(companyToAdd);
            return CreatedAtRoute("GetCompanyById", new { companyId = companyToAdd.Id }, companyToAdd);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public  IActionResult Update([FromBody]CompanyDto companyToUpdate)
        {
            _companyService.CurrentUser = "armand";
            _companyService.Update(companyToUpdate);
            return CreatedAtRoute("GetCompanyById", new { companyId = companyToUpdate.Id }, companyToUpdate);
        }

        [HttpDelete("{companyId}")]
        public  IActionResult Delete(int companyId)
        {
            _companyService.DataBaseAction = DataBaseActionEnum.Delete;
             _companyService.Delete(companyId);
            return NoContent();
        }
    }

}