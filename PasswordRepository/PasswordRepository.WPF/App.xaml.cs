using Dna;
using PasswordRepository.Core;
using PasswordRepository.Relational;
using System.Windows;

namespace PasswordRepository.WPF
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// Custom startup so we load our IoC immediately before anything else
		/// </summary>
		/// <param name="e"></param>
		protected override async void OnStartup(StartupEventArgs e)
		{
			// Let the base application do what it needs
			base.OnStartup(e);

			// Setup the Dna Framework
			Framework.Construct<DefaultFrameworkConstruction>()
				.AddFileLogger()
				.AddPasswordRepositoryServices()
				.AddDataStore()
				.Build();

			// Log it
			FrameworkDI.Logger.LogDebugSource("Application starting...");

			// Ensure the client data store 
			await CoreDI.DataStore.EnsureDataStoreAsync();
		}
	}
}