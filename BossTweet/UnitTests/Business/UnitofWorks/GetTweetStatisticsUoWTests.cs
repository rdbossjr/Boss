using BossTweet.Business.UnitofWorks;
using BossTweet.DataAccess.Contexts;
using BossTweet.DataAccess.Repositories;

namespace BossTweet.UnitTests.Business.UnitofWorks
{
    [TestClass]
    public class GetTweetStatisticsUoWTests : TestBase
    {
        [TestMethod]
        public void GetTweetStatisticsUoWExecuteTest()
        {
            Thread.Sleep(SleepTimeMil);

            var twitterConfig = new TwitterWebServiceContextConfiguration
            {
                BaseURI = "https://api.twitter.com/2/tweets/",

                BearerToken = TwitterBearerToken
            };

            var twitterContext = new TwitterWebServiceContext(twitterConfig)
            {
                EndPointRoute = "sample/stream"
            };

            var twitterRepository = new TwitterWebServiceRepository(twitterContext);

            var uow = new GetTweetStatisticsUoW(new GetNTweetsViaTwitterStreamUoW(twitterRepository))
            {
                NoOfTweetsToGet = 100
            };

            var response = uow.Execute();

            Assert.IsNotNull(response);

            Assert.AreEqual(response.TopTenHashtags.Count, 10);
        }
    }
}