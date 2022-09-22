namespace BossTweet.Core.Business;

public abstract class UnitofWorkBase<T>
{
    public int ReturnCode { get; set; }

    public Exception? ReturnException { get; set; }

    public string? ReturnMessage { get; set; }

    protected T? ReturnObject { get; set; }

    public virtual T Execute()
    {
        try
        {
            ReturnObject = ImplementExecute();
        }
        catch (Exception ex)
        {
            ReturnException = ex;
            throw;
        }

        return ReturnObject;
    }

    protected abstract T ImplementExecute();
}