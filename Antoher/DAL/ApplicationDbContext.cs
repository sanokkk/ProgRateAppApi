using Antoher.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Antoher.DAL
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> users { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Like> likes { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<Request> requests { get; set; }
        public DbSet<Friend> friends { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    //builder.Entity<Friend>().ToTable("friends").HasKey(x => new { x.friendOne_id, x.friendTwo_id });
        //    builder.Entity<Friend>().ToTable("friends").HasNoKey();
        //}
    }

    
}
