namespace BossTweet.Core.Services;

public class ServiceReturnObject<T> : IServiceReturnObject<T>
{
    public ServiceReturnObject()
    {
        ReturnCode = 0;
        ReturnMessage = "Success";
    }

    public int ReturnCode { get; set; }

    public Exception? ReturnException { get; set; }

    public string? ReturnMessage { get; set; }

    public T? ReturnObject { get; set; }
}