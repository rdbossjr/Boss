using BossTweet.Business;
using BossTweet.Business.UnitofWorks;
using BossTweet.DataAccess;
using BossTweet.DataAccess.Contexts;
using BossTweet.DataAccess.Repositories;

namespace BossTweet.UnitTests
{
    [TestClass]
    public class GetTweetStatisticsUoWTests
    {
        [TestMethod]
        public void GetTweetStatisticsUoWExecuteTest()
        {
            Thread.Sleep(2000);

            var twitterConfig = new TwitterWebServiceContextConfiguration
            {
                BaseURI = "https://api.twitter.com/2/tweets/",

                BearerToken = "" //TODO: GET BEARER TOKEN FROM DAVID;
            };

            var twitterContext = new TwitterWebServiceContext(twitterConfig)
            {
                EndPointRoute = "sample/stream"
            };

            var twitterRepository = new TwitterWebServiceRepository(twitterContext);

            var uow = new GetTweetStatisticsUoW(new GetNTweetsViaTwitterStreamUoW(twitterRepository));

            uow.NoOfTweetsToGet = 100;

            var response = uow.Execute();

            Assert.IsNotNull(response);

            Assert.AreEqual(response.TopTenHashtags.Count, 10);
        }
    }
}