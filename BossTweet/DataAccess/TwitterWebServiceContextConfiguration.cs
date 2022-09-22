namespace BossTweet.DataAccess;

public class TwitterWebServiceContextConfiguration : ITwitterWebServiceContextConfiguration
{
    public string? BaseURI { get; set; }

    public string? BearerToken { get; set; }
}