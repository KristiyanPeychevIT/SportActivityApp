namespace SportApp.DataLayer
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using BusinessLayer;

    public class SportAppDbContext : IdentityDbContext<IdentityUser>
    {
        public SportAppDbContext(DbContextOptions<SportAppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Sport> Sports { get; set; } = null!;
    }
}