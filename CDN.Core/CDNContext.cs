using CDN.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CDN.Core;
public class CDNContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entry => { entry.ToTable("Users", tb => tb.HasTrigger("TriggerUsers")); });
    }
}
