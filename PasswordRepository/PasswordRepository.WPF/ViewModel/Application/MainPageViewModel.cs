using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using PasswordRepository.Core;

namespace PasswordRepository.WPF
{
	/// <summary>
	/// The View Model for a main page screen
	/// </summary>
	public class MainPageViewModel : BaseViewModel
	{
		#region Public Properties

		/// <summary>
		/// The server message log
		/// </summary>
		public ObservableCollection<Account> Accounts { get; set; }

		/// <summary>
		/// Selected account from the list
		/// </summary>
		public Account SelectedAccount { get; set; }

		/// <summary>
		/// A flag indicating if the delete command is running
		/// </summary>
		public bool DeleteIsRunning { get; set; }

		/// <summary>
		/// A flag indicating if the refresh command is running
		/// </summary>
		public bool RefreshIsRunning { get; set; }

		#endregion

		#region Commands

		/// <summary>
		/// The command to refresh a list of accounts
		/// </summary>
		public ICommand CreateCommand { get; set; }

		/// <summary>
		/// The command to refresh a list of accounts
		/// </summary>
		public ICommand UpdateCommand { get; set; }

		/// <summary>
		/// The command to refresh a list of accounts
		/// </summary>
		public ICommand DeleteCommand { get; set; }

		/// <summary>
		/// The command to refresh a list of accounts
		/// </summary>
		public ICommand RefreshCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public MainPageViewModel()
		{
			// Create commands
			CreateCommand = new RelayAsyncCommand(CreateAsync);
			UpdateCommand = new RelayAsyncCommand(UpdateAsync);
			DeleteCommand = new RelayAsyncCommand(DeleteAsync);
			RefreshCommand = new RelayAsyncCommand(RefreshAsync);

			// Update accounts
			RefreshCommand.Execute(null);
		}

		#endregion

		#region Commands Methods

		/// <summary>
		/// The command to create new account
		/// </summary>
		public async Task CreateAsync()
		{
			await DI.UI.ManageAccount(new ManageAccountDialogViewModel()
			{
				Title = "Adding new account"
			});
			await RefreshAsync();
		}

		/// <summary>
		/// The command to update selected account
		/// </summary>
		public async Task UpdateAsync()
		{
			await DI.UI.ManageAccount(new ManageAccountDialogViewModel(SelectedAccount)
			{
				Title = "Changing selected account"
			});
			await RefreshAsync();
		}

		/// <summary>
		/// The command to delete selected account
		/// </summary>
		public async Task DeleteAsync()
		{
			await RunCommandAsync(() => DeleteIsRunning, async () =>
			{
				await CoreDI.DataStore.DeleteAccountAsync(SelectedAccount);
			});
			await RefreshAsync();
		}

		/// <summary>
		/// The command to refresh a list of accounts
		/// </summary>
		public async Task RefreshAsync()
		{
			await RunCommandAsync(() => RefreshIsRunning, async () =>
			{
				var accounts = await CoreDI.DataStore.GetAccountsDetailsAsync();
				foreach (var account in accounts)
				{
					account.Password = EncryptionHelper.Decrypt(account.Password);
				}
				Accounts = accounts;
			});
		}

		#endregion
	}
}