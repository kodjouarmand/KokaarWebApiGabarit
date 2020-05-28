using Catsa.Infrastructure.Logging;
using Catsa.Domain.Errors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Catsa.API.Extensions
{
	public static class ExceptionHandlerExtensions
    {
		public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerService logger)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = "application/json";
					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					if (contextFeature != null)
					{
						logger.LogError($"--------------------------------------------");
						logger.LogError($"L'erreur suivante s'est produite : {contextFeature.Error}");

						await context.Response.WriteAsync(new ErrorModel()
						{
							StatusCode = context.Response.StatusCode,
							Message = "Une erreur s'est produite au niveau du serveur. Veuillez consulter les fichiers journaux pour plus de détails."
						}.ToString());
					}
				});
			});
		}
	}
}
