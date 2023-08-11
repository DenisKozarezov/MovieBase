using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Security
{
	internal class PasswordHasher : IPasswordHasher
	{
		private readonly string Password;

		public PasswordHasher(string password)
        {
			Password = password;
        }

		private HashAlgorithm GetHasher(HashType hashType)
		{
			switch (hashType)
			{
				case HashType.MD5:
					return new MD5CryptoServiceProvider();
				case HashType.SHA1:
					return new SHA1Managed();
				case HashType.SHA256:
					return new SHA256Managed();
				case HashType.SHA384:
					return new SHA384Managed();
				case HashType.SHA512:
					return new SHA512Managed();
				default:
					throw new ArgumentException();
			}
		}
		public string CreateHash(HashType hashType)
		{
			if (string.IsNullOrEmpty(Password)) throw new ArgumentNullException();

			HashAlgorithm hasher = GetHasher(hashType);
			byte[] inputBytes = Encoding.UTF8.GetBytes(Password);

			byte[] hashBytes = hasher.ComputeHash(inputBytes);
			StringBuilder hash = new StringBuilder();
			foreach (byte b in hashBytes)
			{
				hash.Append(string.Format("{0:x2}", b));
			}
			return hash.ToString();
		}
	}
}