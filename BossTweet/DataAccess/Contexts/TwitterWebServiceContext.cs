using BossTweet.DataAccess.Interfaces;
using System.Text;

namespace BossTweet.DataAccess.Contexts;

public class TwitterWebServiceContext : ITwitterWebServiceContext
{
    private HttpClient? _client;
    private string? _endpointUrl;
    private bool _isDisposed;

    public TwitterWebServiceContext(ITwitterWebServiceContextConfiguration configuration)
    {
        ContextConfiguration = configuration;
        BaseUrl = configuration.BaseURI;
        BearerAuthorization = configuration.BearerToken;
    }

    public string? BaseUrl { get; set; }

    public string? BearerAuthorization { get; set; }

    public HttpClient Client
    {
        get => _client ??= new HttpClient();
        set => _client = value;
    }

    public HttpContent? Content { get; set; }

    public ITwitterWebServiceContextConfiguration? ContextConfiguration { get; set; }

    public string? EndPointRoute { get; set; }

    public string? EndPointUrl
    {
        get
        {
            if (!string.IsNullOrEmpty(_endpointUrl))
            {
                return _endpointUrl;
            }

            _endpointUrl = BaseUrl + EndPointRoute + BuildQueryString();

            return _endpointUrl;
        }
    }

    public List<KeyValuePair<string, string>> QueryStrings { get; } = new();

    public void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        _isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    private string BuildQueryString()
    {
        var queryString = new StringBuilder("?");

        foreach (var pair in QueryStrings)
        {
            queryString.Append($"{pair.Key}={pair.Value}&");
        }

        return queryString.ToString().TrimEnd('&').TrimEnd('?');
    }
}