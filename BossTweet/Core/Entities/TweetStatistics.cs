namespace BossTweet.Core.Entities;

public class TweetStatistics : ITweetStatistics
{
    public List<string> TopTenHashtags { get; } = new();

    public int TotalNumberOfTweets { get; set; } = 0;
}