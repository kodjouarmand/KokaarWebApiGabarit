using Microsoft.AspNetCore.Mvc;
using Catsa.API.ActionFilters;
using Marvin.Cache.Headers;
using Catsa.Infrastructure.Logging;
using Catsa.BusinessLogic.Enums;
using Catsa.Domain.Assemblers.Proxies;
using Catsa.BusinessLogic.Queries.Proxies;
using Catsa.BusinessLogic.Commands.Proxies;

namespace Catsa.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ProxiesController : ControllerBase
    {        
        private readonly ILoggerService _logger;
        private readonly IProxyQuery _proxyQuery;
        private readonly IProxyCommand _proxyCommand;
        public ProxiesController(ILoggerService logger, IProxyQuery proxyQuery, IProxyCommand proxyCommand)
        {
            _logger = logger;
            _proxyQuery = proxyQuery;
            _proxyCommand = proxyCommand;
        }

        //[HttpGet(Name = "GetProxies"), Authorize(Roles = "Manager")]
        [HttpGet(Name = "GetProxies")]
        public  IActionResult GetAll()
        {
            var proxies =  _proxyQuery.GetAll();
            return Ok(proxies);
        }

        [HttpGet("{proxyId}", Name = "GetProxyById")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        public  IActionResult GetById(int proxyId)
        {
            var proxy =  _proxyQuery.GetById(proxyId);
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
        public  IActionResult Add([FromBody] ProxyCommandDto proxyToAdd)
        {
            _proxyCommand.CurrentUser = "armand";
            _proxyCommand.Add(proxyToAdd);
            return CreatedAtRoute("GetProxyById", new { proxyId = proxyToAdd.Id }, proxyToAdd);
        }

        [HttpPut]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public  IActionResult Update([FromBody] ProxyCommandDto proxyToUpdate)
        {
            _proxyCommand.CurrentUser = "armand";
            _proxyCommand.Update(proxyToUpdate);
            return CreatedAtRoute("GetProxyById", new { proxyId = proxyToUpdate.Id }, proxyToUpdate);
        }

        [HttpDelete("{proxyId}")]
        public  IActionResult Delete(int proxyId)
        {
            _proxyCommand.DataBaseAction = DataBaseActionEnum.Delete;
            _proxyCommand.Delete(proxyId);
            return NoContent();
        }
    }

}