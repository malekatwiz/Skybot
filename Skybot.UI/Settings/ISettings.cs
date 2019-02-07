namespace Skybot.UI.Settings
{
    public interface ISettings
    {
        string SkybotAuthUri { get; }
        string SkybotAuthClientId { get; }
        string SkybotAuthClientSecret { get; }
        string SkybotAccountsUri { get; }
        string SkybotTextoUri { get; }
    }
}
