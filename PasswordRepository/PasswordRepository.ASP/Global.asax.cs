using Dna;
using PasswordRepository.Core;
using PasswordRepository.Relational;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PasswordRepository.ASP
{
	public class MvcApplication : HttpApplication
	{
		protected async void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			// Setup the Dna Framework
			Framework.Construct<DefaultFrameworkConstruction>()
				.AddFileLogger()
				.AddDataStore()
				.Build();

			// Log it
			FrameworkDI.Logger.LogDebugSource("Application starting...");

			// Ensure the client data store 
			await CoreDI.DataStore.EnsureDataStoreAsync();
		}
	}
}
