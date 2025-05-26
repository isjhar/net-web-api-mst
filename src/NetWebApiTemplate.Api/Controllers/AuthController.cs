using Microsoft.AspNetCore.Mvc;
using NetWebApiTemplate.Api.Models;
using NewWebApiTemplate.Application.Dtos;
using NewWebApiTemplate.Application.Exceptions;
using NewWebApiTemplate.Application.Services;

namespace NetWebApiTemplate.Api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _userService.LoginAsync(new UserCredentialsDto
                {
                    Username = request.Username,
                    Password = request.Password,
                });

                return Ok(new ApiResponse<LoginResponse>(new LoginResponse
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken,
                }));
            }
            catch (Exception ex)
            {
                if (ex == AppExceptionFactory.EntityNotFound)
                {
                    throw UserFirendlyExceptionFactory.UsernameOrPasswordIncorrect;
                }
                throw;
            }
        }

        [HttpPost("relogin")]
        public async Task<IActionResult> Relogin([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var result = await _userService.ReloginAsync(request.Token);

                return Ok(new ApiResponse<LoginResponse>(new LoginResponse
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken,
                }));
            }
            catch (Exception ex)
            {
                if (ex == AppExceptionFactory.EntityNotFound)
                {
                    throw UserFirendlyExceptionFactory.InvalidParams;
                }
                throw;
            }
        }
    }
}
