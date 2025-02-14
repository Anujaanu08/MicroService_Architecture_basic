using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using user_Core.entity;
using user_Core.IRepositories;
using User_InfraStructure.database;

namespace User_InfraStructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User>AddUser(User user)
        {
            await _db.users.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
           return await _db.users.ToListAsync();
        }

    }
}
