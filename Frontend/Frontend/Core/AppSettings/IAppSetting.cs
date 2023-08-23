namespace Frontend.Core.AppSettings
{
    public interface IAppSetting
    {
        string BaseUrlApi { get; }
        string DockerBaseApiUrl { get; }
        string GetApiUrl();
    }
}
