using System.Security.Cryptography;
using System.Text;

namespace AvaCrm.Application.Securities;

public class HashingService : IHashingService
{
	public string Hash(string input)
	{
		using var sha256 = SHA256.Create();
		var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
		var builder = new StringBuilder();
		foreach (var b in bytes)
			builder.Append(b.ToString("x2"));
		return builder.ToString();
	}

	public bool Verify(string input, string hash) =>	
		Hash(input) == hash;
	
}
