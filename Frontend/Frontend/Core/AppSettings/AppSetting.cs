using Frontend.Utilities;

namespace Frontend.Core.AppSettings
{
    public class AppSetting : IAppSetting
    {
        private IConfiguration _configuration;

        public AppSetting(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string BaseUrlApi => _configuration[Constants.AppSettings.BaseApiUrl];

    }
}
