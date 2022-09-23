using BossTweet.Core.Business;
using BossTweet.Core.Extensions;
using BossTweet.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BossTweet.Services.Base;

public class BossTweetControllerBase : Controller
{
    protected virtual IActionResult CreateActionResult<T>(IServiceReturnObject<T> returnObject, string? friendlyError = null)
    {
        if (returnObject.ReturnException == null)
        {
            return Ok(returnObject);
        }

        if (!string.IsNullOrWhiteSpace(friendlyError))
        {
            returnObject.ReturnMessage = friendlyError;
        }

        //TODO: more types of errors?
        const int statusCode = (int)HttpStatusCode.InternalServerError;

        return StatusCode(
            statusCode,
            returnObject);
    }

    protected async Task<IServiceReturnObject<T>> ExecuteAndSetServiceReturnObject<T>(IAsyncUnitofWork<T> uow)
    {
        var returnObject = new ServiceReturnObject<T>();

        try
        {
            var returnEntity = await uow.Execute();
            returnObject.ReturnObject = returnEntity;
        }
        catch (AggregateException ex)
        {
            var flattenEx = ex.Flatten();
            SetReturnObjectException(returnObject, flattenEx);
        }
        catch (Exception ex)
        {
            SetReturnObjectException(returnObject, ex);
        }

        return returnObject;
    }

    protected IServiceReturnObject<T> ExecuteAndSetServiceReturnObject<T>(IUnitofWork<T> uow)
    {
        var returnObject = new ServiceReturnObject<T>();

        try
        {
            var returnEntity = uow.Execute();
            returnObject.ReturnObject = returnEntity;
        }
        catch (AggregateException ex)
        {
            var flattenEx = ex.Flatten();
            SetReturnObjectException(returnObject, flattenEx);
        }
        catch (Exception ex)
        {
            SetReturnObjectException(returnObject, ex);
        }

        return returnObject;
    }

    protected void SetReturnObjectException(IServiceReturnException returnObject, Exception ex)
    {
        var message = ex.Message;
        var exception = ex;

        returnObject.ReturnCode = ex.HResult;
        returnObject.ReturnMessage = message;
        returnObject.ReturnException = exception.CreateNewReturnException();
    }
}