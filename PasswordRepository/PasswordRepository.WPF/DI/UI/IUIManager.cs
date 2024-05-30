using System.Threading.Tasks;

namespace PasswordRepository.WPF
{
	/// <summary>
	/// The UI manager that handles any UI interaction in the application
	/// </summary>
	public interface IUIManager
	{
		/// <summary>
		/// Displays a account manage box to the user
		/// </summary>
		/// <param name="viewModel">The view model</param>
		/// <returns></returns>
		Task ManageAccount(ManageAccountDialogViewModel viewModel);

		/// <summary>
		/// Displays a single message box to the user
		/// </summary>
		/// <param name="viewModel">The view model</param>
		/// <returns></returns>
		Task ShowMessage(MessageBoxDialogViewModel viewModel);
	}
}