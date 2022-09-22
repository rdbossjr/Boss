using BossTweet.Business;
using BossTweet.Core.Entities;
using BossTweet.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using BossTweet.DataAccess;

namespace BossTweet.Services.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TweetController : BossTweetControllerBase
{
    private readonly IGetNTweetsViaTwitterStreamUoW _getNTweetsViaTwitterStream;
    private readonly ITwitterWebServiceRepository _repository;
    private readonly IGetTweetStatisticsUoW _getTweetStatisticsUoW;

    public TweetController(
        IGetTweetStatisticsUoW getTweetStatisticsUoW, 
        IGetNTweetsViaTwitterStreamUoW getNTweetsViaTwitterStream,
        ITwitterWebServiceRepository repository)
    {
        _getTweetStatisticsUoW = getTweetStatisticsUoW;
        _getNTweetsViaTwitterStream = getNTweetsViaTwitterStream;
        _repository = repository;
    }

    [Route("{numberOfTweetsToSample}")]
    [HttpGet]
    [ProducesResponseType(typeof(ServiceReturnObject<ITweetStatistics>), (int)HttpStatusCode.OK)]
    public IActionResult GetTweetStatistics(int numberOfTweetsToSample)
    {
        _getTweetStatisticsUoW.NoOfTweetsToGet = numberOfTweetsToSample;
        var returnObject = ExecuteAndSetServiceReturnObject(_getTweetStatisticsUoW);
        return CreateActionResult(returnObject);
    }
}