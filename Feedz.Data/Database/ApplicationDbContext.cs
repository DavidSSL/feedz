using Feedz.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Data.Database;

public class ApplicationDbContext : IdentityDbContext
{
    
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<FeedEntry> FeedEntries { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}

