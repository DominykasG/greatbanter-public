using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class User : IdentityUser<int>
    {
        //public int Id { get; set; }
        //public string Username { get; set; }
        public virtual ICollection<Banter> Banters { get; set; }
    }
}
