using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using user_Core.entity;

namespace user_Core.IRepositories
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<List<User>> GetUsers();
    }
}
