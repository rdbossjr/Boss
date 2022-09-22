using BossTweet.Core.Business;
using BossTweet.Core.Entities;

namespace BossTweet.Business.Interfaces;

public interface IGetTweetStatisticsUoW : IUnitofWork<ITweetStatistics>
{
    int NoOfTweetsToGet { get; set; }
}