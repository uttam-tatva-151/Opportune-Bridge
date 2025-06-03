using Core.Beans;
using Core.DTOs;

namespace Services.Interfaces;

public interface ILoginService
{
    Task<ResponseResult<LoginResult>> AuthenticateUserAsync(LoginRequest request);
}
