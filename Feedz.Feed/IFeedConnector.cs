using System.ServiceModel.Syndication;

namespace Feedz.Feed;

public interface IFeedConnector
{
    SyndicationFeed? Fetch();
}

