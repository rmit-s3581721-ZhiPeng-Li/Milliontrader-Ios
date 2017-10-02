using System;
using System.Security.Cryptography;
using System.Text;

namespace SAILIIOS
{
	public class AccountController
	{
		public AccountController()
		{
		}
	}

	public static class SaltGenerator
	{
		private static RNGCryptoServiceProvider encryptProvider = null;
		private const int SALT_SIZE = 24;

		static SaltGenerator()
		{
			encryptProvider = new RNGCryptoServiceProvider();
		}

		//get Salt
		public static string GetSaltString()
		{
			byte[] saltBytes = new byte[SALT_SIZE];

			encryptProvider.GetBytes(saltBytes);
			return Convert.ToBase64String(saltBytes);
		}

		//get hash
		public static string GenerateSHA256Hash(string input, string salt)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
			SHA256 sha256hashstring = SHA256.Create();

			byte[] hash = sha256hashstring.ComputeHash(bytes);

			string result = BitConverter.ToString(hash).Replace("-", "");

			return result;
		}
	}

	//Definition of Hash Althorim
	public class HashComputer
	{
		public string GetPasswordHashAndSalt(string message)
		{
			SHA256 sha256 = SHA256.Create();
			byte[] target = Encoding.UTF8.GetBytes(message);
			byte[] afterhashtarget = sha256.ComputeHash(target);
			string result = BitConverter.ToString(afterhashtarget).Replace("-", "");
			return result;
		}

	}

	//The class to generate HashAndSalt password for account
	public class PasswordManager
	{
		HashComputer hc = new HashComputer();

		public string GeneratePasswordHash(string plainTextPassword, out string salt)
		{
			salt = SaltGenerator.GetSaltString();
			string finalString = plainTextPassword + salt;
			return hc.GetPasswordHashAndSalt(finalString);
		}

		//Method to check whether the password match
		public bool IsPasswordMatch(string password, string salt, string HashedPassword)
		{
			string finalString = password + salt;
            return HashedPassword == hc.GetPasswordHashAndSalt(finalString);
		}
	}
}
