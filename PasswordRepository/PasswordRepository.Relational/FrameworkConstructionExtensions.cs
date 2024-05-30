using PasswordRepository.Core;
using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace PasswordRepository.Relational
{
	/// <summary>
	/// Extension methods for the <see cref="FrameworkConstruction"/>
	/// </summary>
	public static class FrameworkConstructionExtensions
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public static FrameworkConstruction AddDataStore(this FrameworkConstruction construction)
		{
			// Register the DataStoreDbContext with EF6
			construction.Services.AddTransient<DataStoreDbContext>();

			// Add chat data store for easy access/use of the backing data store
			// Make it scoped so we can inject the scoped DbContext
			construction.Services.AddTransient<IDataStore>(
				provider => new BaseDataStore(provider.GetService<DataStoreDbContext>()));

			// Return framework for chaining
			return construction;
		}
	}
}