using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace PasswordRepository.Core
{
	/// <summary>
	/// Represents an account in the data store.
	/// </summary>
	public class Account
	{
		/// <summary>
		/// Gets or sets the unique identifier for the account.
		/// </summary>
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the account.
		/// </summary>
		/// <exception cref="ValidationException">Thrown if the account name is null or exceeds 100 characters.</exception>
		[Required]
		[StringLength(100)]
		[DisplayName("Account name")]
		public string AccountName { get; set; }

		/// <summary>
		/// Gets or sets the date and time when the account was last updated.
		/// </summary>
		public DateTime LastUpdated { get; set; }

		/// <summary>
		/// Gets or sets the comments associated with the account.
		/// </summary>
		/// <exception cref="ValidationException">Thrown if the comments exceed 500 characters.</exception>
		[StringLength(500)]
		public string Comments { get; set; }

		/// <summary>
		/// Gets or sets the hashed password for the account.
		/// </summary>
		/// <exception cref="ValidationException">Thrown if the password is null.</exception>
		[Required]
		[Column("PasswordHash")]
		public string Password { get; set; }
	}
}