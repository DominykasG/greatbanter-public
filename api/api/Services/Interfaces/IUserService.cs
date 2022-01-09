using api.DTO;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> FindByUserName(string username);
        Task<UserDTO> GetUser(int id);
    }
}
