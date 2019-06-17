using System.Web.Http;
using WebActivatorEx;
using System.Linq;
using XSIS.Marcomm.API;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace XSIS.Marcomm.API
{
	public static class SwaggerConfig
	{
		public static void Register()
		{
			var thisAssembly = typeof(SwaggerConfig).Assembly;

			GlobalConfiguration.Configuration
				.EnableSwagger(c =>
				{
                    c.SingleApiVersion("v1", "XSIS.Marcomm.API");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
				.EnableSwaggerUi(c =>
				{
				});
		}
	}
}
