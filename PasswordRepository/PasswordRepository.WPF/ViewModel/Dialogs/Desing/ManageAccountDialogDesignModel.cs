namespace PasswordRepository.WPF
{
	/// <summary>
	/// The design-time data for a <see cref="ManageAccountDialogViewModel"/>
	/// </summary>
	public class ManageAccountDialogDesignModel : ManageAccountDialogViewModel
	{
		#region Singleton

		/// <summary>
		/// A single instance of the design model
		/// </summary>
		public static ManageAccountDialogDesignModel Instance => new ManageAccountDialogDesignModel();

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public ManageAccountDialogDesignModel()
		{
			AccountName = "Account Name";
			Comments = "Comment; Comment; Comment; Comment; Comment; ";
			Password = "Open password";
		} 

		#endregion
	}
}