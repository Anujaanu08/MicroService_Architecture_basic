using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using user_Core.DTO;
using user_Core.entity;
using user_Core.IRepositories;
using user_Core.IService;

namespace user_Core.Service
{
    public class UserService : IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDTO> AddUser(UserRequestDTO userRequest)
        {
            var user = userRequest.Adapt<User>();
            var userres = await _repository.AddUser(user);
            var response = userres.Adapt<UserResponseDTO>();
            return response;
        }

        public async Task<List<UserResponseDTO>> GetUsers()
        {
            var userlist = await _repository.GetUsers();
           var response =  userlist.Select(u=> u.Adapt<UserResponseDTO>()).ToList();
            return response;
        }

    }
}
