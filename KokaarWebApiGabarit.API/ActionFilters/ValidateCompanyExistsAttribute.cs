using KokaarWebApiGabarit.Infrastructure.Contracts;
using KokaarWebApiGabarit.Persistance.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace KokaarWebApiGabarit.API.ActionFilters
{
    public class ValidateCompanyExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerService _logger;
        public ValidateCompanyExistsAttribute(IRepositoryManager repository, ILoggerService logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT") ? true : false;
            var id = (int)context.ActionArguments["id"];
            var company = await _repository.Company.GetCompanyAsync(id, trackChanges);

            if (company == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("company", company);
                await next();
            }
        }
    }

}
