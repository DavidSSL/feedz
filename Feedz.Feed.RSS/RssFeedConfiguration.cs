namespace Feedz.Feed.RSS;

public class RssFeedConfiguration
{
    public Uri? FeedUri { get; set; }
    public string? HttpAuthentication { get; set; }
    public List<string> RequestHeaders { get; set; } = new();
}