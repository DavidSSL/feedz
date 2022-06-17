using Hangfire;
using Feedz.Feed.RSS;

namespace Feedz.Worker.Jobs;

public class TestFeed
{
    public static string Schedule(Uri? feedUri)
    {
        return BackgroundJob.Enqueue(() => Run(feedUri));
    }

    public static Feedz.Data.Models.Feed Run(Uri? feedUri)
    {
        var connector = new RssFeedConnector();
        connector.Configuration.FeedUri = feedUri;
        var originalFeed = connector.Fetch();

        var feed = new Feedz.Data.Models.Feed(originalFeed);
        return feed;
    }
}