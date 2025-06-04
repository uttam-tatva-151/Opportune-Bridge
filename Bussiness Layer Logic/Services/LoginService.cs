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

    public LoginService(IUserRepository userRepository, IJWTService jWTService)
    {
        _userRepository = userRepository;
        _jwtService = jWTService;
    }
    /// <summary>
    /// Authenticates a user based on their email and password.
    /// </summary>
    /// <param name="request">The login request containing the user's email and password.</param>
    /// <returns>
    /// A <see cref="ResponseResult{LoginResult}"/> object containing the authentication result.
    /// If successful, it includes the generated JWT token; otherwise, it contains the failure reason.
    /// </returns>
    /// <remarks>
    /// This method performs the following steps:
    /// 1. Validates the user's existence using their email.
    /// 2. Checks if the user's account is locked and returns an error if locked.
    /// 3. Verifies the user's password against the stored hash.
    /// 4. Handles failed login attempts, including locking the account if necessary.
    /// 5. Handles successful login attempts by resetting failed attempts and updating the last login time.
    /// 6. Generates a JWT token for the authenticated user.
    /// </remarks>
    public async Task<ResponseResult<LoginResult>> AuthenticateUserAsync(LoginRequest request)
    {
        ResponseResult<LoginResult> result = new();
        try
        {
            // Validate user existence
            UserDTO? user = await _userRepository.GetUserByEmailAsync(request.Email.ToLower().Trim());
            if (user == null || string.IsNullOrWhiteSpace(user.HashPassword) || string.IsNullOrWhiteSpace(user.UserEmail))
            {
                return ResponseResult<LoginResult>.Failure(
                    MessageHelper.GetNotFoundMessage(Constants.USER),
                    ResponseStatus.NotFound
                );
            }

            // Check if the account is locked
            if (IsAccountLocked(user))
            {
                return ResponseResult<LoginResult>.Failure(
                    MessageHelper.GetErrorMessageForLockedAccount(GetRemainingLockDuration(user)),
                    ResponseStatus.Unauthorized
                );
            }

            // Verify password
            if (!VerifyPassword(request.Password, user.HashPassword))
            {
                await HandleFailedLoginAttemptAsync(user);
                return ResponseResult<LoginResult>.Failure(
                    Constants.ERROR_PASSWORD_MISMATCH,
                    ResponseStatus.Unauthorized
                );
            }

            // Handle successful login
            await HandleSuccessfulLoginAsync(user);
            string accessToken =  _jwtService.GenerateAccessToken(user);
        }
        catch (Exception)
        {
            throw;
        }

        return result;
    }

    #region Helper Methods

    /// <summary>
    /// Checks if the user's account is locked.
    /// </summary>
    /// <param name="user">The user object.</param>
    /// <returns>True if the account is locked, otherwise false.</returns>
    private static bool IsAccountLocked(UserDTO user)
    {
        return user.LockedUntil.HasValue && user.LockedUntil.Value > DateTime.Now;
    }

    /// <summary>
    /// Gets the remaining lock duration in minutes.
    /// </summary>
    /// <param name="user">The user object.</param>
    /// <returns>The remaining lock duration in minutes.</returns>
    private static string GetRemainingLockDuration(UserDTO user)
    {
        return user.LockedUntil.HasValue
            ? (user.LockedUntil.Value - DateTime.Now).TotalMinutes.ToString("F0")
            : "0";
    }

    /// <summary>
    /// Verifies the user's password against the stored hash.
    /// </summary>
    /// <param name="password">The plain text password.</param>
    /// <param name="hashPassword">The hashed password.</param>
    /// <returns>True if the password matches, otherwise false.</returns>
    private static bool VerifyPassword(string password, string hashPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }

    /// <summary>
    /// Handles a failed login attempt, including locking the account if necessary.
    /// </summary>
    /// <param name="user">The user object.</param>
    private async Task HandleFailedLoginAttemptAsync(UserDTO user)
    {
        user.FailedAttempts++;
        if (user.FailedAttempts >= Constants.MAX_FAILED_ATTEMPTS)
        {
            user.LockedUntil = DateTime.Now.AddMinutes(Constants.LOCK_DURATION_MINUTES);
            user.FailedAttempts = 0; // Reset failed attempts after locking
            await _userRepository.UpdateUserLockStatusAsync(user);

            throw new Exception(MessageHelper.GetErrorMessageForLockedAccount(Constants.LOCK_DURATION_MINUTES.ToString()));
        }

        await _userRepository.UpdateUserLockStatusAsync(user);
    }

    /// <summary>
    /// Handles a successful login attempt, including resetting failed attempts and updating the last login time.
    /// </summary>
    /// <param name="user">The user object.</param>
    private async Task HandleSuccessfulLoginAsync(UserDTO user)
    {
        user.FailedAttempts = 0; // Reset failed attempts on successful login
        user.LastLoginAt = DateTime.Now;
        await _userRepository.UpdateUserLoginStatusAsync(user);
    }

    #endregion
}