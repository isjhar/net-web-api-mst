using Microsoft.AspNetCore.Mvc;
using NetWebApiTemplate.Api.Attributes;
using NetWebApiTemplate.Api.Models;
using NewWebApiTemplate.Application.Dtos;
using NewWebApiTemplate.Application.Exceptions;
using NewWebApiTemplate.Application.Services;
using NewWebApiTemplate.Domain.Enums;
using System.Security.Claims;

namespace NetWebApiTemplate.Api.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Permission(PermissionKey.ViewUser)]
        [HttpGet()]
        public async Task<IActionResult> GetLoggedUserInfo()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw AppExceptionFactory.Forbidden;

            var userInfo = await _userService.FindUserInfoByIdAsync(Guid.Parse(userId));

            return Json(new ApiResponse<UserInfoDto>(userInfo));
        }
    }
}
