namespace BossTweet.DataAccess.Interfaces;

public interface ITwitterWebServiceContextConfiguration
{
    string? BaseURI { get; set; }

    string? BearerToken { get; set; }
}