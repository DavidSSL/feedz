namespace Feedz.Feed;
using Feedz.Data.Models;

public interface IFeedConnector
{
    Feed Fetch();
}

