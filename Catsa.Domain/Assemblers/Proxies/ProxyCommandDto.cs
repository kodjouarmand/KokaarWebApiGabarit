using System;

namespace Catsa.Domain.Assemblers.Proxies
{
    public class ProxyCommandDto : BaseCommandDto<Guid>
    {
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
