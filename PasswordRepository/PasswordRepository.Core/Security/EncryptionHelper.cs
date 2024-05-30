using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordRepository.Core
{
	/// <summary>
	/// Provides methods for encrypting and decrypting strings using AES encryption.
	/// </summary>
	public static class EncryptionHelper
	{
		/// <summary>
		/// The key used for AES encryption and decryption.
		/// </summary>
		private static readonly byte[] Key = Encoding.UTF8.GetBytes("A1B2C3D4E5F60718293A4B5C6D7E8F90");

		/// <summary>
		/// The initialization vector (IV) used for AES encryption and decryption.
		/// </summary>
		private static readonly byte[] IV = Encoding.UTF8.GetBytes("1A2B3C4D5E6F7G8H");

		/// <summary>
		/// Encrypts a plain text string using AES encryption.
		/// </summary>
		/// <param name="plainText">The plain text to encrypt.</param>
		/// <returns>The encrypted text, encoded as a Base64 string.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the <paramref name="plainText"/> is null or empty.</exception>
		/// <exception cref="CryptographicException">Thrown if an error occurs during the encryption process.</exception>
		public static string Encrypt(string plainText)
		{
			using (var aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (var msEncrypt = new MemoryStream())
				{
					using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					using (var swEncrypt = new StreamWriter(csEncrypt))
					{
						swEncrypt.Write(plainText);
					}

					return Convert.ToBase64String(msEncrypt.ToArray());
				}
			}
		}

		/// <summary>
		/// Decrypts a Base64 encoded cipher text string using AES encryption.
		/// </summary>
		/// <param name="cipherText">The encrypted text, encoded as a Base64 string.</param>
		/// <returns>The decrypted plain text string.</returns>
		/// <exception cref="ArgumentNullException">Thrown if the <paramref name="cipherText"/> is null or empty.</exception>
		/// <exception cref="FormatException">Thrown if the <paramref name="cipherText"/> is not a valid Base64 string.</exception>
		/// <exception cref="CryptographicException">Thrown if an error occurs during the decryption process.</exception>
		public static string Decrypt(string cipherText)
		{
			using (var aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (var msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
				using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
				using (var srDecrypt = new StreamReader(csDecrypt))
				{
					return srDecrypt.ReadToEnd();
				}
			}
		}
	}
}