using BossTweet.DataAccess;

namespace BossTweet.UnitTests;

[TestClass]
public class TwitterWebServiceContextTests
{
    [TestMethod]
    public void EndPointUrlTest()
    {
        var twitterConfig = new TwitterWebServiceContextConfiguration
        {
            BaseURI = "https://api.twitter.com/2/tweets/"
        };

        var twitterContext = new TwitterWebServiceContext(twitterConfig)
        {
            EndPointRoute = "sample/stream"
        };

        Assert.AreEqual(twitterContext.EndPointUrl, $"{twitterConfig.BaseURI}{twitterContext.EndPointRoute}");
    }
}