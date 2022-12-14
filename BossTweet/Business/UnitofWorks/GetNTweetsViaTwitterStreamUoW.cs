using BossTweet.Business.Interfaces;
using BossTweet.Core.Business;
using BossTweet.Core.Entities.Twitter;
using BossTweet.DataAccess.Interfaces;

namespace BossTweet.Business.UnitofWorks;

public class GetNTweetsViaTwitterStreamUoW : UnitofWorkBase<IList<Tweet>>, IGetNTweetsViaTwitterStreamUoW
{
    public GetNTweetsViaTwitterStreamUoW(ITwitterWebServiceRepository repository)
    {
        Repository = repository;
    }

    public int NoOfTweetsToGet { get; set; } = 1000;

    public ITwitterWebServiceRepository Repository { get; }

    protected override IList<Tweet> ImplementExecute()
    {
        Repository.WebServiceContext.EndPointRoute = "sample/stream";

        var returnList = new List<Tweet>();

        var response = Repository.GetStreamSerializedToObjects(NoOfTweetsToGet);

        if (response.Any())
        {
            returnList.AddRange(response);
        }

        return returnList;
    }
}