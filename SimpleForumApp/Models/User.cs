using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleForumApp.Models
{
    public class User
    {
        public User()
        {
            Posts = new List<Post>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public List<Post> Posts;

        
    }
}
