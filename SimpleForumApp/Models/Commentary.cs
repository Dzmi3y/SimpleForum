using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleForumApp.Models
{
    public class Commentary
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int? PostID { get; set; }
        public User Post { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }
    }
}
