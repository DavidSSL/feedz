using Feedz.Data.Database;
using Feedz.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Feedz.Data.Repositories
{
    class FeedRepository : IFeedRepository
    {
        private readonly ApplicationDbContext _db;

        public FeedRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<int> CountFeedEntries()
        {
            return _db.FeedEntries.CountAsync();
        }

        public Task<int> CountFeedEntries(Feed feed)
        {
            return _db.FeedEntries.Where(fe => fe.Feed == feed).CountAsync();
        }

        public Task<int> CountFeedEntryUserStates()
        {
            return _db.FeedEntryUserStates.CountAsync();
        }

        public Task<int> CountFeedEntryUserStates(FeedEntry feedEntry)
        {
            return _db.FeedEntryUserStates.Where(feus => feus.FeedEntry == feedEntry).CountAsync();
        }

        public Task<int> CountFeedEntryUserStates(ApplicationUser user)
        {
            return _db.FeedEntryUserStates.Where(feus => feus.User == user).CountAsync();
        }

        public Task<int> CountFeeds()
        {
            return _db.Feeds.CountAsync();
        }

        public Task<int> CountFeedSubscription(Feed feed)
        {
            return _db.FeedSubscriptions.Where(fs => fs.Feed == feed).CountAsync();
        }

        public Task<int> CountFeedSubscriptions()
        {
            return _db.FeedSubscriptions.CountAsync();
        }

        public Task<int> CountFeedSubscriptions(ApplicationUser user)
        {
            return _db.FeedSubscriptions.Where(fs => fs.User == user).CountAsync();
        }
    }
}
