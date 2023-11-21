using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MicrosoftIdentity.Models;

namespace MicrosoftIdentity.Data
{
    public class DbContextClass : IdentityDbContext<User, Role, string>
    {
        public DbSet<User> Users { get; set; }

        public DbContextClass(DbContextOptions<DbContextClass> options)
       : base(options)
        {
        }
    }
}
