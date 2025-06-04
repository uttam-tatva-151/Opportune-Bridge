using Core.Beans;
using Core.Beans.Enums;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }
    [HttpPost("login")]
    public async Task<ResponseResult<LoginResult>> LoginUser(LoginRequest request)
    {
        ResponseResult<LoginResult> result =  await _loginService.AuthenticateUserAsync(request);
        return result;
    }
}
