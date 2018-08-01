using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleForumApp.Models
{
    public class Post
    {
        public Post()
        {
            Commentaries = new List<Commentary>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }

        public List<Commentary> Commentaries { get; set; }
        
    }
}
