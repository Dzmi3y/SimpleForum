using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace SimpleForumApp.Models
{
    public class ApplicationContext : DbContext,IApplicationContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Commentary> Commentaries { get; set; }

    }
}
