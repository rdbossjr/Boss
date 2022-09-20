using BossTweet.DataAccess;
using System.IO;

namespace BossTweet.UnitTests;

[TestClass]
public class TwitterRepositoryTests
{
    [TestMethod]
    public void CallTwitterStreamTest()
    {
        var twitterConfig = new TwitterWebServiceContextConfiguration();

        twitterConfig.BaseURI = "https://api.twitter.com/2/tweets/";

        twitterConfig.ClientID = "dcHlJKe9U8OF9xLryJZmwLmAg"; //"GET CLIENT ID FROM SEPARATE FILE";

        twitterConfig.ClientSecret = "yg3NhNpi6c9yoCqT1jSB3aEwdR1fQC3DjoMGHao1Kg9k8iv10A"; //"GET CLIENT SECRET FROM SEPARATE FILE";

        var twitterContext = new TwitterWebServiceContext(twitterConfig);

        twitterContext.BearerAuthorization = "AAAAAAAAAAAAAAAAAAAAAN0OhQEAAAAAUNokTxaXSBdMgAwYz4W03fb31HI%3DcEFt4Ia3Ro3S7k16KnbJiIGrijNQI1Pk3zXJ2Xy9iuAbrKqU1f"; //"GET CLIENT SECRET FROM SEPARATE FILE";

        twitterContext.EndPointRoute = "sample/stream";

        var twitterRepository = new TwitterWebServiceRepository(twitterContext);

        var response = twitterRepository.GetStreamSerialized();

        Assert.IsNotNull(response);
    }
}