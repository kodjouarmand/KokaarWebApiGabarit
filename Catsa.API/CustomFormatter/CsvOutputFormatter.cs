using Catsa.Model.DataTransferObjects;
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
            if (typeof(ProxyDto).IsAssignableFrom(type) || typeof(IEnumerable<ProxyDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();
            if (context.Object is IEnumerable<ProxyDto>)
            {
                foreach (var proxy in (IEnumerable<ProxyDto>)context.Object)
                {
                    FormatCsv(buffer, proxy);
                }
            }
            else
            {
                FormatCsv(buffer, (ProxyDto)context.Object);
            }
            await response.WriteAsync(buffer.ToString());
        }
        private static void FormatCsv(StringBuilder buffer, ProxyDto proxy)
        {
            buffer.AppendLine($"{proxy.Id},\"{proxy.Nom},\"{proxy.Description}\"");
        }
    }
}
