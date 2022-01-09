using api.DTO;
using api.Models;
using api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services
{
    public class UserService: IUserService
    {
        private readonly BanterContext _context;
        public UserService(BanterContext context) 
        {
            _context = context;
        }

        public async Task<User> FindByUserName(string username) 
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
        public async Task<UserDTO> GetUser(int id) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            return new UserDTO(user.Id, user.UserName);
        }
    }
}
