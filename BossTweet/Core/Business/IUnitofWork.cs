namespace BossTweet.Core.Business;

public interface IUnitofWork<out T>
{
    T Execute();
}