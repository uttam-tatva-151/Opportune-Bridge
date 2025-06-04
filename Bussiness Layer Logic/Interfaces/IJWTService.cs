using System.Security.Claims;
using Core.Beans;
using Core.DTOs;

namespace Services.Interfaces;

public interface IJWTService
{
    string GenerateAccessToken(UserDTO user);
    string GenerateRefreshToken();
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<ResponseResult<Guid>> SaveRefreshTokenAsync(Guid userId, string refreshToken, DateTime expiresAt, string createdByIp, string userAgent, string deviceId);
    Task<ResponseResult<bool>> ValidateRefreshTokenAsync(Guid userId, string refreshToken, string deviceId);
    Task<ResponseResult<bool>> RevokeRefreshTokenAsync(Guid userId, string refreshToken, string deviceId, string revokedByIp);
    Task<ResponseResult<int>> RemoveExpiredTokensAsync(Guid userId, string? deviceId = null);
    Task<ResponseResult<List<ActiveSessionDTO>>> GetActiveSessionsAsync(Guid userId);
}