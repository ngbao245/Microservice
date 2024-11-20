using Microservices.Services.AuthAPI.Data;
using Microservices.Services.AuthAPI.Models;
using Microservices.Services.AuthAPI.Models.Dto;
using Microservices.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Microservices.Services.AuthAPI.Service
{
	public class AuthService : IAuthService
	{
		private readonly AppDbContext _appDbContext;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;
		public AuthService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
		{
			_appDbContext = appDbContext;
			_userManager = userManager;
			_roleManager = roleManager;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
		{
			var user = _appDbContext.ApplicationUsers.FirstOrDefault(_ => _.UserName.ToLower() == loginRequestDto.UserName.ToLower());

			bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

			if (user == null || !isValid)
			{
				return new LoginResponseDto() { User = null, Token = "" };
			}

			//if user was found, Generate JWT Token
			var token = _jwtTokenGenerator.GenerateToken(user);

			UserDto userDto = new()
			{
				Email = user.Email,
				Id = user.Id,
				Name = user.Name,
				PhoneNumber = user.PhoneNumber
			};

			LoginResponseDto loginResponseDto = new LoginResponseDto()
			{
				User = userDto,
				Token = token
			};
			return loginResponseDto;
		}

		public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
		{
			ApplicationUser user = new()
			{
				UserName = registerationRequestDto.Email,
				Email = registerationRequestDto.Email,
				NormalizedEmail = registerationRequestDto.Email.ToUpper(),
				Name = registerationRequestDto.Name,
				PhoneNumber = registerationRequestDto.PhoneNumber,
			};

			try
			{
				var result = await _userManager.CreateAsync(user, registerationRequestDto.Password);
				if (result.Succeeded)
				{
					var userToReturn = _appDbContext.ApplicationUsers.First(_ => _.UserName == registerationRequestDto.Email);

					UserDto userDto = new()
					{
						Email = userToReturn.Email,
						Id = userToReturn.Id,
						Name = userToReturn.Name,
						PhoneNumber = userToReturn.PhoneNumber,
					};
					return "";
				}
				else
				{
					return result.Errors.FirstOrDefault().Description;
				}
			}
			catch (Exception ex)
			{
			}
			return "Error Encountered";
		}
	}
}
