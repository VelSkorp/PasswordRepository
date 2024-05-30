using PasswordRepository.Core;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordRepository.Relational
{
	/// <summary>
	/// Provides a base implementation for a data store using Entity Framework.
	/// </summary>
	public class BaseDataStore : IDataStore
	{
		#region Protected Members

		/// <summary>
		/// The database context for the client data store
		/// </summary>
		protected DataStoreDbContext mDbContext;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="dbContext">The database to use</param>
		public BaseDataStore(DataStoreDbContext dbContext)
		{
			mDbContext = dbContext;
		}

		#endregion

		#region Interface Implementation

		public async Task<bool> EnsureDataStoreAsync()
		{
			// Make sure the database exists and is created
			return await Task.FromResult(mDbContext.Database.CreateIfNotExists());
		}

		public async Task<ObservableCollection<Account>> GetAccountsDetailsAsync()
		{
			var accounts = await mDbContext.Accounts.AsNoTracking().ToListAsync();
			return new ObservableCollection<Account>(accounts);
		}

		public async Task<bool> SaveAccountAsync(Account account)
		{
			var existingAccountName = await mDbContext.Accounts.AsNoTracking()
				.Where(acc => acc.AccountName.Equals(account.AccountName) && acc.Id != account.Id)
				.Select(acc => acc.AccountName)
				.FirstOrDefaultAsync();

			if (!string.IsNullOrEmpty(existingAccountName))
			{
				return false;
			}

			var existingAccount = await mDbContext.Accounts.SingleOrDefaultAsync(acc => acc.Id == account.Id);
			if (existingAccount != null)
			{
				// Update existing
				existingAccount.AccountName = account.AccountName;
				existingAccount.LastUpdated = account.LastUpdated;
				existingAccount.Comments = account.Comments;
				existingAccount.Password = account.Password;
			}
			else
			{
				// Add new one
				mDbContext.Accounts.Add(account);
			}

			// Save changes
			return await mDbContext.SaveChangesAsync() == 1;
		}

		public async Task<bool> DeleteAccountAsync(Account account)
		{
			var existingAccount = await mDbContext.Accounts.FindAsync(account.Id);

			if (existingAccount is null)
			{
				return false;
			}

			mDbContext.Accounts.Remove(existingAccount);

			// Save changes
			return await mDbContext.SaveChangesAsync() == 1;
		}

		#endregion
	}
}