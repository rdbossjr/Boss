namespace BossTweet.DataAccess.Interfaces;

public interface ITwitterWebServiceContext : IDisposable
{
    string? BaseUrl { get; set; }

    string? BearerAuthorization { get; set; }

    HttpClient Client { get; set; }

    HttpContent? Content { get; set; }

    ITwitterWebServiceContextConfiguration? ContextConfiguration { get; set; }

    string? EndPointRoute { get; set; }

    string? EndPointUrl { get; }

    List<KeyValuePair<string, string>> QueryStrings { get; }
}