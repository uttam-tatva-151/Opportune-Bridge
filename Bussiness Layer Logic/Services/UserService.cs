using Core.DTOs;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<RoleWiseUseDeatilsDTO>> GetRoleWiseUseDeatils()
    {
        List<RoleWiseUseDeatilsDTO> roles = await _userRepository.GetRoleWiseUserListAsync();

        foreach (RoleWiseUseDeatilsDTO role in roles)
        {
            foreach (RoleWiseUseDeatilsDTO.UserDTO user in role.Users)
            {
                if (!string.IsNullOrEmpty(user.HashedPassword))
                {
                    user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);
                }
            }
        }
        return roles;
    }

}
