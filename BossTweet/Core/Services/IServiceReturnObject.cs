namespace BossTweet.Core.Services;

public interface IServiceReturnObject<T> : IServiceReturnException, IActionParameter
{
    T? ReturnObject { get; set; }
}