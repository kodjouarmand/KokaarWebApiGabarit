using Catsa.Domain.Assemblers;
using Catsa.Domain.Assemblers.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Catsa.API.CustomFormatter
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type type)
        {
            if (typeof(ProxyQueryDto).IsAssignableFrom(type) || typeof(IEnumerable<ProxyQueryDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<ProxyQueryDto>)
            {
                foreach (var proxy in (IEnumerable<ProxyQueryDto>)context.Object)
                {
                    FormatCsv(buffer, proxy);
                }
            }
            else
            {
                FormatCsv(buffer, (ProxyQueryDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, ProxyQueryDto proxy)
        {
            buffer.AppendLine($"{proxy.Id},\"{proxy.Nom},\"{proxy.Description}\"");
        }
    }
}
