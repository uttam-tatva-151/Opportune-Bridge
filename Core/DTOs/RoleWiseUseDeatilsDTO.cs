namespace Core.DTOs;

public class RoleWiseUseDeatilsDTO
{
   public string RoleName { get; set; } = null!; // Name of the role
   public int UserCount { get; set; } // Number of users associated with the role
   public List<UserDTO> Users { get; set; } = new(); // Array of users associated with the role 

    public class UserDTO
    {
        public string UserName { get; set; } = null!; // Name of the user
        public string Email { get; set; } = null!; // Email of the user 
        public string HashedPassword { get; set; } = null!; // Hashed password of the user
    }

}
