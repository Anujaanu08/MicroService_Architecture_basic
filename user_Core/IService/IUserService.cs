
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user_Core.DTO;

namespace user_Core.IService
{
    public interface IUserService
    {
        Task<UserResponseDTO> AddUser(UserRequestDTO userRequest);
        Task<List<UserResponseDTO>> GetUsers();
    }
}
