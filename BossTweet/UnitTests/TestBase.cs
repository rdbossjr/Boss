namespace BossTweet.UnitTests;

public class TestBase
{
    // Added sleep (10 seconds) to some TestMethods due Twitter Service exception:
    // Response status code does not indicate success: 429 (Too Many Requests).
    // When run by itself, it passes
    protected int SleepTimeMil = 10000;

    protected string TwitterBearerToken = ""; //TODO: GET BEARER TOKEN FROM DAVID;
}