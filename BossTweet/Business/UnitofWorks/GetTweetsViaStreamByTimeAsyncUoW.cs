using BossTweet.Business.Interfaces;
using BossTweet.Core.Business;
using BossTweet.Core.Entities.Twitter;
using BossTweet.DataAccess.Interfaces;

namespace BossTweet.Business.UnitofWorks;

public class GetTweetsViaStreamByTimeAsyncUoW : AsyncUnitofWorkBase<IList<Tweet>>, IGetTweetsViaStreamByTimeAsyncUoW
{
    public GetTweetsViaStreamByTimeAsyncUoW(ITwitterWebServiceRepository repository)
    {
        Repository = repository;
    }

    public int NoOfMilliseconds { get; set; } = 1000;

    public ITwitterWebServiceRepository Repository { get; }

    protected override async Task<IList<Tweet>> ImplementExecute()
    {
        Repository.WebServiceContext.EndPointRoute = "sample/stream";

        var returnList = new List<Tweet>();

        var response = await Repository.GetStreamSerializedToObjectsOverTime(NoOfMilliseconds);

        if (response.Any())
        {
            returnList.AddRange(response);
        }

        return returnList;
    }
}