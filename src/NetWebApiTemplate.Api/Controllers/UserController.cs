using Microsoft.AspNetCore.Mvc;
using NetWebApiTemplate.Api.Attributes;
using NetWebApiTemplate.Api.Models;
using NewWebApiTemplate.Domain.Enums;

namespace NetWebApiTemplate.Api.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        [Permission(PermissionKey.ViewUser)]
        [HttpGet()]
        public async Task<IActionResult> GetLoggedUser()
        {
            return Json(new ApiResponse<string>("Test"));
        }
    }
}
