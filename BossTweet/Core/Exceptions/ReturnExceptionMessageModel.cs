namespace BossTweet.Core.Exceptions;

public class ReturnExceptionMessageModel : IReturnExceptionMessageModel
{
    public string? Message { get; set; }

    public List<string> StackTrace { get; } = new();

    public string? Type { get; set; }
}