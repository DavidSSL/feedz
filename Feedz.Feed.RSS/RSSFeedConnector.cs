using System.Xml;
using Microsoft.Extensions.DependencyInjection;

namespace Feedz.Feed.RSS;
using Feedz.Data.Models;
using System.ServiceModel.Syndication;

public class RSSFeedConnector : IFeedConnector
{
    public RSSFeedConfiguration Configuration { get; set; } = new RSSFeedConfiguration();
    public SyndicationFeed? Fetch()
    {
        var xmlReader = new XmlTextReader(this.Configuration.FeedUri.ToString());
        var feed = SyndicationFeed.Load(xmlReader);
        return feed;
    }
}

