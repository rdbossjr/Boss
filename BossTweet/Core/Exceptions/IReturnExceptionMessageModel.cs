namespace BossTweet.Core.Exceptions;

public interface IReturnExceptionMessageModel
{
    string? Message { get; set; }

    List<string> StackTrace { get; }

    string? Type { get; set; }
}