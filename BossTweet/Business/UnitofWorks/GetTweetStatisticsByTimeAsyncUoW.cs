using BossTweet.Business.Interfaces;
using BossTweet.Core.Business;
using BossTweet.Core.Entities;

namespace BossTweet.Business.UnitofWorks;

public class GetTweetStatisticsByTimeAsyncUoW : AsyncUnitofWorkBase<ITweetStatistics>, IGetTweetStatisticsByTimeAsyncUoW
{
    private readonly IGetTweetsViaStreamByTimeAsyncUoW _getTweetsViaStreamByTimeAsyncUoW;

    public GetTweetStatisticsByTimeAsyncUoW(IGetTweetsViaStreamByTimeAsyncUoW getNTweetsViaTwitterStreamUoW)
    {
        _getTweetsViaStreamByTimeAsyncUoW = getNTweetsViaTwitterStreamUoW;
    }

    public int NoOfMilliseconds { get; set; } = 1000;

    protected override async Task<ITweetStatistics> ImplementExecute()
    {
        _getTweetsViaStreamByTimeAsyncUoW.Repository.WebServiceContext.QueryStrings.Add(
                new KeyValuePair<string, string>(
                    "tweet.fields",
                    "entities,id"));

        _getTweetsViaStreamByTimeAsyncUoW.NoOfMilliseconds = NoOfMilliseconds;

        var tweets = await _getTweetsViaStreamByTimeAsyncUoW.Execute();

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
}