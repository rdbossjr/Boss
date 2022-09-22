using BossTweet.Business;
using BossTweet.DataAccess;

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

                BearerToken = "AAAAAAAAAAAAAAAAAAAAAN0OhQEAAAAAUNokTxaXSBdMgAwYz4W03fb31HI%3DcEFt4Ia3Ro3S7k16KnbJiIGrijNQI1Pk3zXJ2Xy9iuAbrKqU1f" //"GET CLIENT SECRET FROM SEPARATE FILE";
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