using Microsoft.AspNetCore.Mvc;
using Catsa.Domain.Assemblers;
using Catsa.API.ActionFilters;
using Marvin.Cache.Headers;
using Catsa.Infrastructure.Contracts;
using Catsa.BusinessLogic.Contracts;
using Catsa.BusinessLogic.Enums;

namespace Catsa.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ProxiesController : ControllerBase
    {
        private readonly IProxyService _proxyService;
        private readonly ILoggerService _logger;
        public ProxiesController(ILoggerService logger, IProxyService proxyService)
        {
            _logger = logger;
            _proxyService = proxyService;
        }

        //[HttpGet(Name = "GetProxies"), Authorize(Roles = "Manager")]
        [HttpGet(Name = "GetProxies")]
        public  IActionResult GetAll()
        {
            var proxies =  _proxyService.GetAll();
            return Ok(proxies);
        }

        [HttpGet("{proxyId}", Name = "GetProxyById")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public  IActionResult GetById(int proxyId)
        {
            var proxy =  _proxyService.GetById(proxyId);
            if (proxy == null)
            {
                _logger.LogInfo($"Proxy with id: {proxyId} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                return Ok(proxy);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public  IActionResult Add([FromBody]ProxyDto proxyToAdd)
        {
            _proxyService.CurrentUser = "armand";
             _proxyService.Add(proxyToAdd);
            return CreatedAtRoute("GetProxyById", new { proxyId = proxyToAdd.Id }, proxyToAdd);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public  IActionResult Update([FromBody]ProxyDto proxyToUpdate)
        {
            _proxyService.CurrentUser = "armand";
            _proxyService.Update(proxyToUpdate);
            return CreatedAtRoute("GetProxyById", new { proxyId = proxyToUpdate.Id }, proxyToUpdate);
        }

        [HttpDelete("{proxyId}")]
        public  IActionResult Delete(int proxyId)
        {
            _proxyService.DataBaseAction = DataBaseActionEnum.Delete;
             _proxyService.Delete(proxyId);
            return NoContent();
        }
    }

}