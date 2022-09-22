using BossTweet.Core.Entities.Twitter;

namespace BossTweet.DataAccess.Interfaces;

public interface ITwitterWebServiceRepository : IDisposable
{
    ITwitterWebServiceContext WebServiceContext { get; set; }

    List<Tweet> GetStreamSerializedToObjects(int numberOfObjects = 100);
}