using System.IO;
using System.Text;

namespace BossTweet.DataAccess;

public class TwitterWebServiceRepository
{
    public TwitterWebServiceRepository(TwitterWebServiceContext webServiceContext)
    {
        WebServiceContext = webServiceContext;
    }

    protected TwitterWebServiceRepository()
    {
    }

    public TwitterWebServiceContext WebServiceContext { get; set; }

    public async Task<T> CallEndPoint<T>(HttpMethod method)
    {
        var returnResult = await WebServiceContext.CallEndPoint<T>(method);

        return returnResult;
    }

    public void Dispose(bool disposing)
    {
        if (disposing)
        {
            WebServiceContext?.Dispose();
        }
    }

    public string GetStreamSerialized()
    {
        var streamResult = new StringBuilder();

        using (HttpClient httpClient = new HttpClient())
        {
            if (!string.IsNullOrWhiteSpace(WebServiceContext.BearerAuthorization))
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    WebServiceContext.BearerAuthorization);
            }

            httpClient.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
            var requestUri = WebServiceContext.EndPointUrl;
            var stream = httpClient.GetStreamAsync(requestUri).Result;

            using (var reader = new StreamReader(stream))
            {
                for (int i = 0; i < 100; i++)

                {

                    //We are ready to read the stream
                    streamResult.AppendLine(reader.ReadLine());
                }
            }
        }

        return streamResult.ToString();
    }
}