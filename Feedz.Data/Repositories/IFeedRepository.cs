using Feedz.Data.Models;

namespace Feedz.Data.Repositories
{
    internal interface IFeedRepository
    {
        Task<int> CountFeeds();
        Task<int> CountFeedEntries();
        Task<int> CountFeedEntries(Feed feed);
        Task<int> CountFeedSubscriptions();
        Task<int> CountFeedSubscription(Feed feed);
        Task<int> CountFeedSubscriptions(ApplicationUser user);
        Task<int> CountFeedEntryUserStates();
        Task<int> CountFeedEntryUserStates(FeedEntry feedEntry);
        Task<int> CountFeedEntryUserStates(ApplicationUser user);
    }
}
