using Dna;

namespace PasswordRepository.WPF
{
	/// <summary>
	/// The IoC container for our application
	/// </summary>
	public static partial class DI
	{
		/// <summary>
		/// A shortcut to access the <see cref="IUIManager"/>
		/// </summary>
		public static IUIManager UI => Framework.Service<IUIManager>();
	}
}