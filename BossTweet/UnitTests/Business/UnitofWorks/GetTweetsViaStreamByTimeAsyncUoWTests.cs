using BossTweet.Business.UnitofWorks;
using BossTweet.DataAccess.Contexts;
using BossTweet.DataAccess.Repositories;

namespace BossTweet.UnitTests.Business.UnitofWorks
{
    [TestClass]
    public class GetTweetsViaStreamByTimeAsyncUoWTests : TestBase
    {
        [TestMethod]
        public void GetTweetsViaStreamByTimeAsyncUoWExecuteTest()
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

            var uow = new GetTweetsViaStreamByTimeAsyncUoW(twitterRepository)
            {
                NoOfMilliseconds = 5000
            };

            var response = uow.Execute();

            Assert.IsNotNull(response);
        }
    }
}