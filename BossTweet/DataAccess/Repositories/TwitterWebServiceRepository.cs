using BossTweet.Core.Entities.Twitter;
using BossTweet.DataAccess.Interfaces;
using Newtonsoft.Json;
using System.Text;

namespace BossTweet.DataAccess.Repositories;

public class TwitterWebServiceRepository : ITwitterWebServiceRepository
{
    private bool _isDisposed;

    public TwitterWebServiceRepository(ITwitterWebServiceContext webServiceContext)
    {
        WebServiceContext = webServiceContext;
    }

    public ITwitterWebServiceContext WebServiceContext { get; set; }

    public void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            WebServiceContext?.Dispose();
        }

        _isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    //todo: refactor to use generics in baseclass for future reuse by other repositories
    /// <summary>
    /// Retrieves n number of objects from a streaming web service.
    /// </summary>
    /// <param name="numberOfObjects">maximum number of objects of type T to be returned</param>
    /// <returns>List of T</returns>
    public List<Tweet> GetStreamSerializedToObjects(int numberOfObjects = 100)
    {
        var returnList = new List<Tweet>();
        var httpClient = WebServiceContext.Client;
        var requestUri = WebServiceContext.EndPointUrl;

        if (!string.IsNullOrWhiteSpace(WebServiceContext.BearerAuthorization))
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                WebServiceContext.BearerAuthorization);
        }

        httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

        var stream = httpClient.GetStreamAsync(requestUri).Result;

        using var reader = new StreamReader(stream);

        for (var i = 0; i < numberOfObjects; i++)
        {
            var lineRead = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(lineRead)) continue;

            var tDeserialized = JsonConvert.DeserializeObject<Tweet>(lineRead, new JsonSerializerSettings());

            returnList.Add(tDeserialized);
        }

        return returnList;
    }

    public async Task<List<Tweet>> GetStreamSerializedToObjectsOverTime(int milliseconds = 10000)
    {
        var returnList = new List<Tweet>();
        var httpClient = WebServiceContext.Client;
        var requestUri = WebServiceContext.EndPointUrl;
        var returnBuilder = new StringBuilder("[");

        if (!string.IsNullOrWhiteSpace(WebServiceContext.BearerAuthorization))
        {
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                WebServiceContext.BearerAuthorization);
        }

        httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

        var stream = await httpClient.GetStreamAsync(requestUri);

        using var reader = new StreamReader(stream);

        var endTime = DateTime.Now.AddMilliseconds(milliseconds);

        while (DateTime.Now < endTime)
        {
            var lineRead = await reader.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(lineRead)) continue;

            returnBuilder.AppendLine($"{lineRead},");
        }

        var builderString = $"{returnBuilder.ToString().TrimEnd(',')}]";

        var tDeserialized = JsonConvert.DeserializeObject<List<Tweet>>(builderString, new JsonSerializerSettings());

        returnList.AddRange(tDeserialized);

        return returnList;
    }
}