using PasswordRepository.Core;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PasswordRepository.WPF
{
	/// <summary>
	/// Details for a message box dialog
	/// </summary>
	public class ManageAccountDialogViewModel : BaseDialogViewModel
	{
		#region Public Properties

		/// <summary>
		/// The unique account name
		/// </summary>
		public string AccountName { get; set; }

		/// <summary>
		/// The comments for account
		/// </summary>
		public string Comments { get; set; }

		/// <summary>
		/// The account password
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// A flag indicating if the save command is running
		/// </summary>
		public bool SaveIsRunning { get; set; }

		#endregion

		#region Private Members

		/// <summary>
		/// The unique account Id
		/// </summary>
		private int Id;

		#endregion

		#region Commands

		/// <summary>
		/// The command to refresh a list of accounts
		/// </summary>
		public ICommand SaveCommand { get; set; }

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ManageAccountDialogViewModel()
		{
			// Create commands
			SaveCommand = new RelayAsyncCommand(SaveAsync);
		}

		public ManageAccountDialogViewModel(Account account)
			: this()
		{
			Id = account.Id;
			AccountName = account.AccountName;
			Comments = account.Comments;
			Password = account.Password;
		}

		#endregion

		#region Commands Methods


		/// <summary>
		/// The command to delete selected account
		/// </summary>
		public async Task SaveAsync()
		{
			var isSaved = false;
			await RunCommandAsync(() => SaveIsRunning, async () =>
			{
				isSaved = await CoreDI.DataStore.SaveAccountAsync(new Account()
				{
					Id = Id,
					AccountName = AccountName,
					LastUpdated = DateTime.Now,
					Comments = Comments,
					Password = EncryptionHelper.Encrypt(Password)
				});
				AccountName = null;
				Comments = null;
				Password = null;
			});

			await DI.UI.ShowMessage(new MessageBoxDialogViewModel()
			{
				Title = isSaved ? "Success" : "Fail",
				Message = isSaved ? "Account saved successfully." : "Failed to save the account."
			});
		}

		#endregion
	}
}