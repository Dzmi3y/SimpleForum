using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleForumApp.Models
{
    public interface IApplicationContext
    {
      

         DbSet<Role> Roles { get; set; }
          DbSet<User> Users { get; set; }
          DbSet<Post> Posts { get; set; }
          DbSet<Commentary> Commentaries { get; set; }

        int SaveChanges();
    }
}
