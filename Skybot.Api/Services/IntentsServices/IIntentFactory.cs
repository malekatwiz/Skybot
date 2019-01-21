namespace Skybot.Api.Services.IntentsServices
{
    public interface IIntentFactory
    {
        Intent CreateIntent(string intentName);
    }
}
