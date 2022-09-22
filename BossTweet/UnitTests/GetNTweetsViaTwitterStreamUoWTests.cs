using BossTweet.Business;
using BossTweet.Business.UnitofWorks;
using BossTweet.DataAccess;
using BossTweet.DataAccess.Contexts;
using BossTweet.DataAccess.Repositories;

namespace BossTweet.UnitTests;

[TestClass]
public class GetNTweetsViaTwitterStreamUoWTests
{
    [TestMethod]
    public void GetNTweetsViaTwitterStreamUoWExecuteTest()
    {
        Thread.Sleep(2000);

        var twitterConfig = new TwitterWebServiceContextConfiguration
        {
            BaseURI = "https://api.twitter.com/2/tweets/",

            BearerToken = "" //TODO: GET BEARER TOKEN FROM DAVID;
        };

        var twitterContext = new TwitterWebServiceContext(twitterConfig);

        twitterContext.EndPointRoute = "sample/stream";

        var twitterRepository = new TwitterWebServiceRepository(twitterContext);

        var uow = new GetNTweetsViaTwitterStreamUoW(twitterRepository)
        {
            NoOfTweetsToGet = 100
        };

        var response = uow.Execute();

        Assert.IsNotNull(response);

        Assert.AreEqual(response.Count, 100);
    }
}