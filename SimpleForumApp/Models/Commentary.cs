using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleForumApp.Models
{
    public class Commentary
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public Post Post { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
