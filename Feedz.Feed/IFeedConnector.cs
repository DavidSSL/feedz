using System.ServiceModel.Syndication;

namespace Feedz.Feed;
using Feedz.Data.Models;

public interface IFeedConnector
{
    SyndicationFeed? Fetch();
}

