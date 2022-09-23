using BossTweet.Core.Business;
using BossTweet.Core.Entities.Twitter;
using BossTweet.DataAccess.Interfaces;

namespace BossTweet.Business.Interfaces;

public interface IGetTweetsViaStreamByTimeAsyncUoW : IAsyncUnitofWork<IList<Tweet>>
{
    int NoOfMilliseconds { get; set; }

    ITwitterWebServiceRepository Repository { get; }
}