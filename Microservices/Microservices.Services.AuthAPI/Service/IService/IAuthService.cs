using Microservices.Services.AuthAPI.Models.Dto;

namespace Microservices.Services.AuthAPI.Service.IService
{
	public interface IAuthService
	{
		Task<string> Register(RegisterationRequestDto registerationRequestDto);
		Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
	}
}
