using System.Xml;

namespace Feedz.Feed.RSS;
using System.ServiceModel.Syndication;

public class RssFeedConnector : IFeedConnector
{
    public RssFeedConfiguration Configuration { get; set; } = new();
    public SyndicationFeed? Fetch()
    {
        var xmlReader = new XmlTextReader(Configuration.FeedUri.ToString());
        var feed = SyndicationFeed.Load(xmlReader);
        return feed;
    }
}

