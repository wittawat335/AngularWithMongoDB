using Frontend.Utilities;

namespace Frontend.Core.AppSettings
{
    public class AppSetting : IAppSetting
    {
        private IConfiguration _configuration;
        private readonly IHostEnvironment _env;

        public AppSetting(IConfiguration configuration, IHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public string BaseUrlApi => _configuration[Constants.AppSettings.BaseApiUrl];
        public string DockerBaseApiUrl => _configuration[Constants.AppSettings.DockerBaseApiUrl];
        public string GetApiUrl()
        {
            string path = "";
            if (_env.IsDevelopment())
                path = BaseUrlApi;
            else if (_env.IsProduction())
                path = DockerBaseApiUrl;

            return path;
        }
    }
}
