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

    /// <summary>
    /// Retrieves a list of users grouped by their roles.
    /// </summary>
    /// <returns>A list of RoleWiseUseDeatilsDTO objects.</returns>
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
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <returns>A UserDTO object if found, otherwise null.</returns>
    public async Task<UserDTO?> GetUserByEmailAsync(string email)
    {
        try
        {
            return await _appDbContext.UserAuths
                .Include(r => r.User)
                .Include(u => u.Role)
                .AsNoTracking()
                .Where(u => u.Email.ToLower() == email && (u.User.IsActive ?? true))
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
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Updates the lock status and failed attempts of a user.
    /// </summary>
    /// <param name="user">The UserDTO object containing updated lock status.</param>
    public async Task UpdateUserLockStatusAsync(UserDTO user)
    {
        try
        {
            UserAuth? userAuth = await GetUserByUserId(user.UserId);

            if (userAuth != null)
            {
                userAuth.FailedAttempts = user.FailedAttempts;
                userAuth.LockedUntil = user.LockedUntil;

                _appDbContext.UserAuths.Update(userAuth);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception(MessageHelper.GetNotFoundMessage(Constants.USER));
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// Updates the login status and failed attempts of a user.
    /// </summary>
    /// <param name="user">The UserDTO object containing updated login status.</param>
    public async Task UpdateUserLoginStatusAsync(UserDTO user)
    {
        try
        {
            UserAuth? userAuth = await GetUserByUserId(user.UserId);

            if (userAuth != null)
            {
                userAuth.FailedAttempts = user.FailedAttempts;
                userAuth.LastLoginAt = user.LastLoginAt;

                _appDbContext.UserAuths.Update(userAuth);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception(MessageHelper.GetNotFoundMessage(Constants.USER));
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

     public async Task<UserAuth?> GetUserByUserId(Guid userId){
        try
        {
            return await _appDbContext.UserAuths
                .Include(u => u.User)
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId && (u.User.IsActive ?? true) && (u.Role.IsActive ?? true));
        }
        catch (Exception)
        {
            throw;
        }
     }

}
