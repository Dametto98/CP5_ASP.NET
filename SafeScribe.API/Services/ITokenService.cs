using SafeScribe.API.Models;

namespace SafeScribe.API.Services
{
	public interface ITokenService
	{
		// Contrato para o m�todo que vai gerar o token de login
		string GenerateToken(User user);
	}
}