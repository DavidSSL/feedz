using Feedz.Data.Database;
using Feedz.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Feedz.Feed;

public class FeedManager
{
    private readonly ILogger<FeedManager> _logger;
    private readonly ApplicationDbContext _db;

    public FeedManager(ILogger<FeedManager> logger, ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    public async Task<Data.Models.Feed> GetOrRegisterFeed(Uri feedUri)
    {
        var feed = await _db.Feeds.SingleOrDefaultAsync(f => f.Uri == feedUri);
        if (feed == null)
        {
            var newFeed = new Data.Models.Feed()
            {
                Uri = feedUri
            };
            var feedTransaction = await _db.Feeds.AddAsync(newFeed);
            feed = feedTransaction.Entity;
            await _db.SaveChangesAsync();
        }
        return feed;
    }

    public async Task<Data.Models.FeedSubscription> SubscribeToFeed(Uri feedUri, ApplicationUser user)
    {
        var feed = await GetOrRegisterFeed(feedUri);
        var subscription = new FeedSubscription()
        {
            User = user,
            Feed = feed
        };

        var result = await _db.FeedSubscriptions.AddAsync(subscription);
        await _db.SaveChangesAsync();

        return result.Entity;
    }
}