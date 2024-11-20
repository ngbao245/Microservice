using Microservices.Services.AuthAPI.Models;

namespace Microservices.Services.AuthAPI.Service.IService
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(ApplicationUser applicationUser);
	}
}
