using System.Security.Claims;
using System.Security.Cryptography;
using Core.Beans;
using Core.DTOs;
using Services.Interfaces;

namespace Services.Services;

public class JWTService : IJWTService
{
    public string GenerateAccessToken(UserDTO user)
    {
        throw new NotImplementedException();
    }

    public string GenerateRefreshToken()
    {
        byte[] randomBytes = new byte[64]; // 64 bytes = 512 bits
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        return Convert.ToBase64String(randomBytes);
    }

    public Task<ResponseResult<List<ActiveSessionDTO>>> GetActiveSessionsAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseResult<int>> RemoveExpiredTokensAsync(Guid userId, string? deviceId = null)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseResult<bool>> RevokeRefreshTokenAsync(Guid userId, string refreshToken, string deviceId, string revokedByIp)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseResult<Guid>> SaveRefreshTokenAsync(Guid userId, string refreshToken, DateTime expiresAt, string createdByIp, string userAgent, string deviceId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseResult<bool>> ValidateRefreshTokenAsync(Guid userId, string refreshToken, string deviceId)
    {
        throw new NotImplementedException();
    }

}
