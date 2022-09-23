namespace BossTweet.Core.Business;

public abstract class AsyncUnitofWorkBase<T> : IAsyncUnitofWork<T>
{
    public int ReturnCode { get; set; }

    public Exception? ReturnException { get; set; }

    public string? ReturnMessage { get; set; }

    public T? ReturnObject { get; set; }

    public virtual async Task<T> Execute()
    {
        try
        {
            ReturnObject = await ImplementExecute();
        }
        catch (Exception ex)
        {
            ReturnException = ex;
            throw;
        }

        return ReturnObject;
    }

    protected abstract Task<T> ImplementExecute();
}