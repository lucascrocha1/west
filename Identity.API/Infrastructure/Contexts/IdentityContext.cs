namespace Identity.API.Infrastructure.Contexts
{
    using Identity.API.Model;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(opts =>
            {
                opts.Property(e => e.Name).HasMaxLength(255);
            });

            base.OnModelCreating(builder);
        }
    }
}