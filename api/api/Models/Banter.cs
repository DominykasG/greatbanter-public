using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Banter
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool DisplayUsername { get; set; }
    }
}
