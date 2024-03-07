using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using ToDoList.Models;

namespace ToDoList.Data

{

    public class ApplicationDbContext : IdentityDbContext<AppUser>

    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

        {

        }

        public DbSet<DoList> DoLists { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<SubTask> SubTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);

        }

    }

}