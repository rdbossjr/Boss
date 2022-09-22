using BossTweet.Core.Business;
using BossTweet.Core.Entities;

namespace BossTweet.Business;

public interface IGetTweetStatisticsUoW: IUnitofWork<ITweetStatistics>
{
    int NoOfTweetsToGet { get; set; }
}