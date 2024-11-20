using Microservices.Services.AuthAPI.Models;
using Microservices.Services.AuthAPI.Models.Dto;
using Microservices.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.AuthAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		protected ResponseDto _response;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
			_response = new ResponseDto();
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterationRequestDto model)
		{
			var errorMessage = await _authService.Register(model);
			if (!string.IsNullOrEmpty(errorMessage))
			{
				_response.IsSuccess = false;
				_response.Message = errorMessage;
				return BadRequest(_response);
			}
			return Ok(_response);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
		{
			var loginResponse = await _authService.Login(model);
			if (loginResponse.User == null)
			{
				_response.IsSuccess = false;
				_response.Message = "Username or password is incorrect";
				return BadRequest(_response);
			}
			_response.Result = loginResponse;
			return Ok(_response);
		}
	}
}
