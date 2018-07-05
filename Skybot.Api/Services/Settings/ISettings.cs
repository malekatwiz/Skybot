namespace Skybot.Api.Services.Settings
{
    public interface ISettings
    {
        string LuisAppEndpoint { get; }
        string LuisAppKey { get; }
        string LuisAppUri { get; }
        string TranslateApiKey { get; }
        string Auth0TokenUri { get; }
        double IntentThreshold { get; }
    }
}
