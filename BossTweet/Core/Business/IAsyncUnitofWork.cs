namespace BossTweet.Core.Business;

public interface IAsyncUnitofWork<T>
{
    Task<T> Execute();
}