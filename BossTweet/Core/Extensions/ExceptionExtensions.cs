using BossTweet.Core.Exceptions;

namespace BossTweet.Core.Extensions;

public static class ExceptionExtensions
{
    public static Exception CreateNewReturnException(this Exception ex)
    {
        if (ex is AggregateException aggregateException)
        {
            var innerExceptions = new List<Exception>();

            var returnExceptions = aggregateException.InnerExceptions.Select(
                   aggregateExceptionInnerException => aggregateExceptionInnerException.CreateNewReturnException())
               .ToList();

            if (returnExceptions.Any())
            {
                innerExceptions.AddRange(returnExceptions);
            }

            return new AggregateException(innerExceptions);
        }

        var stringSeparators = new[] { "\r\n" };
        var stacktrace = ex.StackTrace?.Split(stringSeparators, StringSplitOptions.None);
        var type = ex.GetType();

        var exception = new ReturnExceptionMessageModel
        {
            Type = type.ToString(),
            Message = ex.Message
        };

        if (stacktrace != null)
        {
            exception.StackTrace?.AddRange(stacktrace);
        }

        Exception? innerException = null;

        if (ex?.InnerException != null)
        {
            innerException = ex.InnerException.CreateNewReturnException();
        }

        return new Exception(exception.SerializeToJson(), innerException);
    }
}