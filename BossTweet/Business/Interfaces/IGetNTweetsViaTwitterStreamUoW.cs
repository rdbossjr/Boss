using BossTweet.Core.Business;
using BossTweet.Core.Entities.Twitter;
using BossTweet.DataAccess.Interfaces;

namespace BossTweet.Business.Interfaces;

public interface IGetNTweetsViaTwitterStreamUoW : IUnitofWork<IList<Tweet>>
{
    int NoOfTweetsToGet { get; set; }

    ITwitterWebServiceRepository Repository { get; }
}