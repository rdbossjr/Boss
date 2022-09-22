using BossTweet.DataAccess.Interfaces;

namespace BossTweet.DataAccess.Contexts;

public class TwitterWebServiceContextConfiguration : ITwitterWebServiceContextConfiguration
{
    public string? BaseURI { get; set; }

    public string? BearerToken { get; set; }
}