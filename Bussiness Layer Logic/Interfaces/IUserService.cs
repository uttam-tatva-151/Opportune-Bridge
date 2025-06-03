using Core.DTOs;

namespace Services.Interfaces;

public interface IUserService
{
    Task<List<RoleWiseUseDeatilsDTO>> GetRoleWiseUseDeatils();
}
