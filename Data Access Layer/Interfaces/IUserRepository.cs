using Core.DTOs;

namespace Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<RoleWiseUseDeatilsDTO>> GetRoleWiseUserListAsync();
    Task<UserDTO?> GetUserByEmailAsync(string email);
    Task UpdateUserLockStatusAsync(UserDTO user);
    Task UpdateUserLoginStatusAsync(UserDTO user);
    Task<UserAuth?> GetUserByUserId(Guid userId);

}
