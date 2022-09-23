using BossTweet.Business.UnitofWorks;
using BossTweet.DataAccess.Contexts;
using BossTweet.DataAccess.Repositories;

namespace BossTweet.UnitTests.Business.UnitofWorks
{
    [TestClass]
    public class GetTweetStatisticsByTimeAsyncUoWTests : TestBase
    {
        [TestMethod]
        public async Task GetTweetStatisticsByTimeAsyncUoWExecuteTest()
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

            var uow = new GetTweetStatisticsByTimeAsyncUoW(new GetTweetsViaStreamByTimeAsyncUoW(twitterRepository))
            {
                NoOfMilliseconds = 5000
            };

            var response = await uow.Execute();

            Assert.IsNotNull(response);

            Assert.AreEqual(response.TopTenHashtags.Count, 10);
        }
    }
}