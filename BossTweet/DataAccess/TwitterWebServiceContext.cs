namespace BossTweet.DataAccess;

public class TwitterWebServiceContext
{
    protected const string NoEndPointUrlMessage = "EndPointUrl must be set before this method can be used.";
    private const string BearerAuthHeaderType = "Bearer";
    private HttpClient? _client;
    private string? _endpointUrl;

    public TwitterWebServiceContext(TwitterWebServiceContextConfiguration configuration)
    {
        ContextConfiguration = configuration;
        BaseUrl = configuration.BaseURI;
    }

    public string BaseUrl { get; set; }

    public string BearerAuthorization { get; set; }

    public HttpClient Client
    {
        get => _client ??= new HttpClient();
        set => _client = value;
    }

    public HttpContent Content { get; set; }

    public TwitterWebServiceContextConfiguration ContextConfiguration { get; set; }

    public string EndPointRoute { get; set; }

    public string EndPointUrl
    {
        get
        {
            if (!string.IsNullOrEmpty(_endpointUrl))
            {
                return _endpointUrl;
            }

            _endpointUrl = BaseUrl + EndPointRoute + QueryString;

            return _endpointUrl;
        }
    }

    public string? QueryString { get; set; }

    public async Task<T> CallEndPoint<T>(HttpMethod? method = null)
    {
        if (string.IsNullOrWhiteSpace(EndPointUrl))
        {
            throw new NullReferenceException(NoEndPointUrlMessage);
        }

        method ??= HttpMethod.Post;

        object? returnResult = null;

        using var message = new HttpRequestMessage(
            method,
            EndPointUrl)
        {
            Content = Content
        };

        if (!string.IsNullOrWhiteSpace(BearerAuthorization))
        {
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                BearerAuthHeaderType,
                BearerAuthorization);
        }

        using var response = await Client.SendAsync(message);

        response.EnsureSuccessStatusCode();

        if (typeof(T) == typeof(string))
        {
            returnResult = await response.Content.ReadAsStringAsync();
        }

        return (T)returnResult;
    }

    public T DeSerialize<T>(string serializedObject)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    public string Serialize<T>(T value)
    {
        throw new NotImplementedException();
    }
}