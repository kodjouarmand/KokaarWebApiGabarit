using Microsoft.AspNetCore.Mvc;
using Catsa.API.ActionFilters;
using Marvin.Cache.Headers;
using Catsa.Infrastructure.Logging;
using Catsa.BusinessLogic.Enums;
using Catsa.Domain.Assemblers.Proxies;
using Catsa.BusinessLogic.Queries.Proxies;
using Catsa.BusinessLogic.Commands.Proxies;
using System;
using System.Linq;

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
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _proxyQuery = proxyQuery ?? throw new ArgumentNullException(nameof(proxyQuery));
            _proxyCommand = proxyCommand ?? throw new ArgumentNullException(nameof(proxyCommand));
        }

        //[HttpGet(Name = "GetProxies"), Authorize(Roles = "Manager")]
        [HttpGet(Name = "GetProxies")]
        public  IActionResult GetAll()
        {
            var proxies =  _proxyQuery.GetAll();
            if (proxies == null || proxies.Count() == 0)
            {
                var message = $"Aucun enregistrement trouvé.";
                _logger.LogInfo(message);
                return NotFound(message);
            }
            else
            {
                return Ok(proxies);
            }
        }

        [HttpGet("{proxyId}", Name = "GetProxyById")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 0)]
        [HttpCacheValidation(MustRevalidate = false)]
        public  IActionResult GetById(int proxyId)
        {
            var proxy =  _proxyQuery.GetById(proxyId);
            if (proxy == null)
            {
                var message = $"Le proxy dont l'id est << {proxyId} >> n'existe pas.";
                _logger.LogInfo(message);
                return NotFound(message);
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