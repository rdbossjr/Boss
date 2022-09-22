namespace BossTweet.DataAccess;

public interface ITwitterWebServiceContextConfiguration
{
    string? BaseURI { get; set; }

    string? BearerToken { get; set; }
}