using Core.Beans;
using Core.Beans.Enums;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    [Route("GetRoleWiseUsers")]
    
    public async Task<ResponseResult<List<RoleWiseUseDeatilsDTO>>> GetRoleWiseUsers()
    {
        ResponseResult<List<RoleWiseUseDeatilsDTO>> result = new();
        try
        {

            result.Data = await _userService.GetRoleWiseUseDeatils();
            result.Status = ResponseStatus.Success;
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
            result.Status = ResponseStatus.Failed;
        }
        return result;
    }
}
