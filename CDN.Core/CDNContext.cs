using CDN.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CDN.Core;
public class CDNContext(DbContextOptions options) : DbContext(options)
{
    public virtual DbSet<User> Users { get; set; }
}
