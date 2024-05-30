using System.Globalization;
using System.Windows.Controls;

namespace PasswordRepository.WPF
{
	/// <summary>
	/// A validation rule that checks if a given value is not null or empty.
	/// </summary>
	public class NotNullOrEmptyValidationRule : ValidationRule
	{
		/// <summary>
		/// Validates whether the specified value is not null or empty.
		/// </summary>
		/// <param name="value">The value to validate.</param>
		/// <param name="cultureInfo">The culture information to use in the validation.</param>
		/// <returns>
		/// A <see cref="ValidationResult"/> indicating whether the validation was successful.
		/// Returns a valid result if the value is not null or empty; otherwise, returns an invalid result with an error message.
		/// </returns>
		public override ValidationResult Validate(object value, CultureInfo cultureInfo)
		{
			if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
			{
				return new ValidationResult(false, "Field can't be empty"); 
			}

			return ValidationResult.ValidResult;
		}
	}
}