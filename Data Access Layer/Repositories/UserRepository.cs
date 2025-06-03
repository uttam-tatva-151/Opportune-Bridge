using Core.Beans;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task<List<RoleWiseUseDeatilsDTO>> GetRoleWiseUserListAsync()
    {
        try
        {
            IQueryable<RoleWiseUseDeatilsDTO> query = _appDbContext.Roles
            .Include(r => r.UserAuths)
            .GroupBy(r => r.RoleId)
            .AsNoTracking()
            .Select(group => new RoleWiseUseDeatilsDTO
            {
                RoleName = group.FirstOrDefault().RoleName ?? Constants.GUEST_ROLE,
                Users = group.SelectMany(r => r.UserAuths.Select(u => new RoleWiseUseDeatilsDTO.UserDTO
                {
                    UserName = u.User.Username,
                    Email = u.Email,
                    HashedPassword = u.PasswordHash
                })).ToList()
            });

            return await query.ToListAsync();
        }
        catch
        {
            throw;
        }
    }

    public async Task<UserDTO?> GetUserByEmailAsync(string email)
    {
        return await _appDbContext.UserAuths
                                    .Include(r => r.User)
                                    .Include(u => u.Role)
                                    .AsNoTracking()
                                    .Where(u => u.Email.ToLower() == email && (u.User.IsActive?? true))
                                    .Select(u => new UserDTO
                                    {
                                        UserId = u.UserId,
                                        UserName = u.User.Username,
                                        UserEmail = u.Email,
                                        HashPassword = u.PasswordHash,
                                        RoleId = u.RoleId,
                                        RoleName = u.Role.RoleName,
                                        LastLoginAt = u.LastLoginAt,
                                        FailedAttempts = u.FailedAttempts,
                                        LockedUntil = u.LockedUntil,
                                        ProfilePicture = u.User.Avatar
                                    }).FirstOrDefaultAsync();
    }
}
