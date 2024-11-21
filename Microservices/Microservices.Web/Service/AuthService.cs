using Microservices.Web.Models;
using Microservices.Web.Service.IService;
using Microservices.Web.Utility;

namespace Microservices.Web.Service
{
	public class AuthService : IAuthService
	{
		private readonly IBaseService _baseService;
		public AuthService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> AssignRoleAsync(RegisterationRequestDto registerationRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = StaticDetail.ApiType.POST,
				Data = registerationRequestDto,
				Url = StaticDetail.AuthAPIBase + "Auth/assign-role"
			});
		}

		public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = StaticDetail.ApiType.POST,
				Data = loginRequestDto,
				Url = StaticDetail.AuthAPIBase + "Auth/login"
			});
		}

		public async Task<ResponseDto?> RegisterAsync(LoginRequestDto loginRequestDto)
		{
			return await _baseService.SendAsync(new RequestDto()
			{
				ApiType = StaticDetail.ApiType.POST,
				Data = loginRequestDto,
				Url = StaticDetail.AuthAPIBase + "Auth/register"
			});
		}
	}
}
