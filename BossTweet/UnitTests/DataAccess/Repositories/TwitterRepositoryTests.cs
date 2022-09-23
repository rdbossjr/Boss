using BossTweet.DataAccess.Contexts;
using BossTweet.DataAccess.Repositories;

namespace BossTweet.UnitTests.DataAccess.Repositories;

[TestClass]
public class TwitterRepositoryTests : TestBase
{
    [TestMethod]
    public void GetStreamSerializedNoQueryStringTest()
    {
        Thread.Sleep(SleepTimeMil);

        var twitterConfig = new TwitterWebServiceContextConfiguration
        {
            BaseURI = "https://api.twitter.com/2/tweets/",

            BearerToken = TwitterBearerToken
        };

        var twitterContext = new TwitterWebServiceContext(twitterConfig);

        twitterContext.EndPointRoute = "sample/stream";

        var twitterRepository = new TwitterWebServiceRepository(twitterContext);

        var response = twitterRepository.GetStreamSerializedToObjects();

        Assert.IsNotNull(response);

        Assert.AreEqual(response.Count, 100);
    }

    [TestMethod]
    public async Task GetStreamSerializedToObjectsOverTimeTest()
    {
        Thread.Sleep(SleepTimeMil);

        var twitterConfig = new TwitterWebServiceContextConfiguration
        {
            BaseURI = "https://api.twitter.com/2/tweets/",

            BearerToken = TwitterBearerToken
        };

        var twitterContext = new TwitterWebServiceContext(twitterConfig);

        twitterContext.EndPointRoute = "sample/stream";

        var twitterRepository = new TwitterWebServiceRepository(twitterContext);

        var response = await twitterRepository.GetStreamSerializedToObjectsOverTime();

        Assert.IsNotNull(response);
    }

    // Added sleep due Twitter Service exception:
    // Response status code does not indicate success: 429 (Too Many Requests).
    // When run by itself, it passes
    [TestMethod]
    public void GetStreamSerializedWithQueryStringTest()
    {
        Thread.Sleep(SleepTimeMil);

        var twitterConfig = new TwitterWebServiceContextConfiguration
        {
            BaseURI = "https://api.twitter.com/2/tweets/",

            BearerToken = TwitterBearerToken
        };

        var twitterContext = new TwitterWebServiceContext(twitterConfig);

        twitterContext.EndPointRoute = "sample/stream";

        twitterContext.QueryStrings.Add(
            new KeyValuePair<string, string>(
                "tweet.fields",
                "attachments,author_id,context_annotations,conversation_id,created_at,entities,geo,id,in_reply_to_user_id,lang,possibly_sensitive,public_metrics,referenced_tweets,reply_settings,source,text,withheld"));

        twitterContext.QueryStrings.Add(
            new KeyValuePair<string, string>(
                "expansions",
                "attachments.poll_ids,attachments.media_keys,author_id,geo.place_id,in_reply_to_user_id,referenced_tweets.id,entities.mentions.username,referenced_tweets.id.author_id"));

        twitterContext.QueryStrings.Add(
            new KeyValuePair<string, string>(
                "media.fields",
                "duration_ms,height,media_key,preview_image_url,public_metrics,type,url,width"));

        twitterContext.QueryStrings.Add(
            new KeyValuePair<string, string>(
                "poll.fields",
                "duration_minutes,end_datetime,id,options,voting_status"));

        twitterContext.QueryStrings.Add(
            new KeyValuePair<string, string>(
                "place.fields",
                "contained_within,country,country_code,full_name,geo,id,name,place_type"));

        twitterContext.QueryStrings.Add(
            new KeyValuePair<string, string>(
                "user.fields",
                "created_at,description,entities,id,location,name,pinned_tweet_id,profile_image_url,protected,public_metrics,url,username,verified,withheld"));

        var twitterRepository = new TwitterWebServiceRepository(twitterContext);

        var response = twitterRepository.GetStreamSerializedToObjects(1);

        Assert.IsNotNull(response);

        Assert.AreEqual(response.Count, 1);
    }
}