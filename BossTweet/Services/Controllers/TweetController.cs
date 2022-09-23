using BossTweet.Business.Interfaces;
using BossTweet.Core.Entities;
using BossTweet.Core.Services;
using BossTweet.Services.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BossTweet.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TweetController : BossTweetControllerBase
{
    private readonly IGetTweetStatisticsUoW _getTweetStatisticsUoW;
    private readonly IGetTweetsViaStreamByTimeAsyncUoW _getTweetsViaStreamByTimeAsyncUoW;

    public TweetController(
        IGetTweetStatisticsUoW getTweetStatisticsUoW,
        IGetTweetsViaStreamByTimeAsyncUoW getTweetsViaStreamByTimeAsyncUoW)
    {
        _getTweetStatisticsUoW = getTweetStatisticsUoW;
        _getTweetsViaStreamByTimeAsyncUoW = getTweetsViaStreamByTimeAsyncUoW;
    }

    [Route("StatsByNumberOfTweets/{numberOfTweetsToSample}")]
    [HttpGet]
    [ProducesResponseType(typeof(ServiceReturnObject<ITweetStatistics>), (int)HttpStatusCode.OK)]
    public IActionResult GetTweetStatistics(int numberOfTweetsToSample = 1000)
    {
        _getTweetStatisticsUoW.NoOfTweetsToGet = numberOfTweetsToSample;
        var returnObject = ExecuteAndSetServiceReturnObject(_getTweetStatisticsUoW);
        return CreateActionResult(returnObject);
    }

    [Route("StatsByStreamTime/{millisecondsToStream}")]
    [HttpGet]
    [ProducesResponseType(typeof(ServiceReturnObject<ITweetStatistics>), (int)HttpStatusCode.OK)]
    public IActionResult GetTweetStatisticsByTime(int millisecondsToStream = 10000)
    {
        _getTweetsViaStreamByTimeAsyncUoW.NoOfMilliseconds = millisecondsToStream;
        var returnObject = ExecuteAndSetServiceReturnObject(_getTweetStatisticsUoW);
        return CreateActionResult(returnObject);
    }
}