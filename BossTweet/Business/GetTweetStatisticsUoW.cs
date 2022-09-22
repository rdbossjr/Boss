using BossTweet.Core.Business;
using BossTweet.Core.Entities;

namespace BossTweet.Business;

public class GetTweetStatisticsUoW : UnitofWorkBase<ITweetStatistics>, IGetTweetStatisticsUoW
{
    private readonly IGetNTweetsViaTwitterStreamUoW _getNTweetsViaTwitterStreamUoW;

    public GetTweetStatisticsUoW(IGetNTweetsViaTwitterStreamUoW getNTweetsViaTwitterStreamUoW)
    {
        _getNTweetsViaTwitterStreamUoW = getNTweetsViaTwitterStreamUoW;
    }

    protected override ITweetStatistics ImplementExecute()
    {
        _getNTweetsViaTwitterStreamUoW.Repository.WebServiceContext.QueryStrings.Add(
                new KeyValuePair<string, string>(
                    "tweet.fields",
                    "entities,id"));

        _getNTweetsViaTwitterStreamUoW.NoOfTweetsToGet = NoOfTweetsToGet;

        var tweets = _getNTweetsViaTwitterStreamUoW.Execute();

        var tweetStatistics = new TweetStatistics
        {
            TotalNumberOfTweets = tweets.Count
        };

        var hashtags = new List<string>();

        foreach (var tweet in tweets)
        {
            if (tweet.Data?.Entities?.Hashtags != null)
            {
                hashtags.AddRange(tweet.Data.Entities.Hashtags.Select(tag => tag.Tag));
            }
        }

        var groupedTags = hashtags.GroupBy(tag => tag).OrderByDescending(tag => tag.Count());

        tweetStatistics.TopTenHashtags.AddRange(groupedTags.Select(tag => tag.Key).Take(10));

        return tweetStatistics;
    }

    public int NoOfTweetsToGet { get; set; } = 1000;
}