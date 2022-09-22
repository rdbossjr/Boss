using Newtonsoft.Json;

namespace BossTweet.Core.Extensions;

public static class ObjectExtensions
{
    public static string SerializeToJson(this object currentObject, JsonSerializerSettings? serializeSettings = null)
    {
        return JsonConvert.SerializeObject(
            currentObject,
            currentObject.SetJsonSerializerSettings(serializeSettings));
    }

    public static JsonSerializerSettings SetJsonSerializerSettings(this object currentObject, JsonSerializerSettings? serializeSettings = null)
    {
        if (serializeSettings != null)
        {
            return serializeSettings;
        }

        serializeSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };

        return serializeSettings;
    }
}