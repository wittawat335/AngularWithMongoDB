using Demo.Core.Interfaces;
using Demo.Domain.Utilities;
using Microsoft.Extensions.Configuration;

namespace Demo.Core.Services
{
    public class AppSettings : IAppSettings
    {
        private IConfiguration _configuration;

        public AppSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Client_URL => _configuration[Constants.AppSettings.Client_URL];

        public string key => _configuration[Constants.AppSettings.JWT.Key];

        public string TestEnv => _configuration[Constants.AppSettings.CheckEnvironment];
    }
}
