using BossTweet.Core.Business;
using BossTweet.Core.Entities;

namespace BossTweet.Business.Interfaces;

public interface IGetTweetStatisticsByTimeAsyncUoW : IAsyncUnitofWork<ITweetStatistics>
{
    int NoOfMilliseconds { get; set; }
}