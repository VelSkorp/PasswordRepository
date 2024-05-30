using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PasswordRepository.Core
{
	/// <summary>
	/// Provides a base implementation for a data store using Entity Framework.
	/// </summary>
	public interface IDataStore
	{
		/// <summary>
		/// Ensures the data store is created and available.
		/// </summary>
		/// <returns>A task that represents the asynchronous operation.</returns>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if there is an error updating the database.</exception>
		Task<bool> EnsureDataStoreAsync();

		/// <summary>
		/// Gets a list of all account details asynchronously.
		/// </summary>
		/// <returns>A task representing the asynchronous operation. The task result contains a list of accounts.</returns>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if an error occurs while retrieving data from the database.</exception>
		Task<ObservableCollection<Account>> GetAccountsDetailsAsync();

		/// <summary>
		/// Saves the specified account asynchronously. If the account exists, it is updated; otherwise, it is added.
		/// </summary>
		/// <param name="account">The account to save.</param>
		/// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the account was saved successfully.</returns>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if an error occurs while saving data to the database.</exception>
		Task<bool> SaveAccountAsync(Account account);

		/// <summary>
		/// Deletes the specified account asynchronously.
		/// </summary>
		/// <param name="account">The account to delete.</param>
		/// <returns>A task representing the asynchronous operation. The task result contains a boolean indicating whether the account was deleted successfully.</returns>
		/// <exception cref="OperationCanceledException">Thrown if the operation is canceled.</exception>
		/// <exception cref="DbUpdateException">Thrown if an error occurs while deleting data from the database.</exception>
		Task<bool> DeleteAccountAsync(Account account);
	}
}