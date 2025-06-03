using Core.Beans;
using Core.Beans.Enums;
using Core.DTOs;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly IJWTService _jwtService;
    public LoginService(IUserRepository userRepository, IJWTService jWTService){
        _userRepository = userRepository;
        _jwtService = jWTService;
    }

    public async Task<ResponseResult<LoginResult>> AuthenticateUserAsync(LoginRequest request){
        ResponseResult<LoginResult> result = new ();
        try{
            UserDTO? user = await _userRepository.GetUserByEmailAsync(request.Email.ToLower().Trim());
            if(user == null){
                return ResponseResult<LoginResult>.Failure(
                MessageHelper.GetNotFoundMessage(Constants.USER),
                ResponseStatus.NotFound
                );
            }
            if(user.LockedUntil.HasValue && user.LockedUntil.Value > DateTime.Now){
                return ResponseResult<LoginResult>.Failure(
                    MessageHelper.GetErrorMessageForLockedAccount(
                        (user.LockedUntil.Value - DateTime.Now).TotalMinutes.ToString("F0")
                    ),
                    ResponseStatus.Unauthorized
                );
            }
            string HashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password, user.HashPassword);
            if(!BCrypt.Net.BCrypt.Verify(request.Password, user.HashPassword) ){
                user.FailedAttempts++;
                if(user.FailedAttempts >= Constants.MAX_FAILED_ATTEMPTS){
                    user.LockedUntil = DateTime.Now.AddMinutes(Constants.LOCK_DURATION_MINUTES);
                    user.FailedAttempts = 0; // Reset failed attempts after locking
                }
                return ResponseResult<LoginResult>.Failure(
                    Constants.ERROR_PASSWORD_MISMATCH,
                    ResponseStatus.Unauthorized
                );
            }
        }catch(Exception){
            throw;
        }
        return result;
    }
}
