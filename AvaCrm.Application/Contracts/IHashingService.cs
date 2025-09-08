namespace AvaCrm.Application.Contracts;

public interface IHashingService
{
	string Hash(string input);
	bool Verify(string input, string hash);
}
