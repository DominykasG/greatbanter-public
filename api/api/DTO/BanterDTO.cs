using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public class BanterDTO
    {
        public BanterDTO() 
        {

        }
        public BanterDTO(int id, string content, int score, int userId, string username) {
            Id = id;
            Content = content;
            Score = score;
            UserId = userId;
            User = new UserDTO(userId, username);
        }
        public int Id { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
    }
}
