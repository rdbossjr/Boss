using BossTweet.Core.Business;
using BossTweet.Core.Entities.Twitter;
using BossTweet.DataAccess.Interfaces;

namespace BossTweet.Business.Interfaces;

public interface IGetNTweetsViaTwitterStreamUoW : IUnitofWork<IList<Tweet>>
{
    ITwitterWebServiceRepository Repository { get; }

    int NoOfTweetsToGet { get; set; }

}