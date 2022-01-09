using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public class UserDTO
    {
        public UserDTO(int id, string username) {
            Id = id;
            Username = username;
        }
        public int Id { get; set; }
        public string Username { get; set; }
    }
}
