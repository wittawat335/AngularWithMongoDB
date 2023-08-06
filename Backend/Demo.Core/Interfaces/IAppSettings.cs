using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Interfaces
{
    public interface IAppSettings
    {
        string Client_URL { get; }
        string key { get; }
        string TestEnv { get; }
    }
}
