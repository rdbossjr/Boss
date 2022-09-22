namespace BossTweet.Core.Entities;

public interface ITweetStatistics
{
    List<string> TopTenHashtags { get; }

    int TotalNumberOfTweets { get; set; }
}