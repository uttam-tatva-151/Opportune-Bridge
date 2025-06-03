using Core.DTOs;

namespace Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<RoleWiseUseDeatilsDTO>> GetRoleWiseUserListAsync();
    Task<UserDTO?> GetUserByEmailAsync(string email);

}
