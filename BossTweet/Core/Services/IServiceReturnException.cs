namespace BossTweet.Core.Services;

public interface IServiceReturnException
{
    int ReturnCode { get; set; }

    Exception? ReturnException { get; set; }

    string? ReturnMessage { get; set; }
}