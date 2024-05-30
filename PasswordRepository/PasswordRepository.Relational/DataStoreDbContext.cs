using PasswordRepository.Core;
using System.Data.Entity;

namespace PasswordRepository.Relational
{
	/// <summary>
	/// The database context for the data store
	/// </summary>
	public class DataStoreDbContext : DbContext
	{
		#region DbSets 

		/// <summary>
		/// The accounts details
		/// </summary>
		public DbSet<Account> Accounts { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public DataStoreDbContext() : base("name=DataStoreConnection") { }

		#endregion

		#region Model Creating

		/// <summary>
		/// Configures the database structure and relationships
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Fluent API

			// Configure tables
			// --------------------------
			//
			// Set primary keys
			modelBuilder.Entity<Account>().ToTable("Accounts").HasKey(a => a.Id);
		}

		#endregion
	}
}