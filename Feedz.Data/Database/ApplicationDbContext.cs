using Feedz.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Data.Database;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    
    public DbSet<Feed> Feeds { get; set; }
    public DbSet<FeedEntry> FeedEntries { get; set; }
    public DbSet<FeedEntryUserState> FeedEntryUserStates { get; set; }
    public DbSet<FeedSubscription> FeedSubscriptions { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}

