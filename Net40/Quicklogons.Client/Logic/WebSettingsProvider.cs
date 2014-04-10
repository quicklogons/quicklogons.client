using Quicklogons.Client.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client.Logic
{
    public class WebSettingsProvider : ISettingsProvider
    {
        private Settings _settings = null;

        public ISettings GetSettings()
        {
            if (_settings != null) return _settings;
            int v = int.TryParse(ConfigurationManager.AppSettings["Quicklogons.TimestampWindow"], out v) ? v : 60;
            return _settings = new Settings 
            {
                Url = ConfigurationManager.AppSettings["Quicklogons.Url"],
                SecureUrl = ConfigurationManager.AppSettings["Quicklogons.SecureUrl"],
                SiteKey = ConfigurationManager.AppSettings["Quicklogons.SiteKey"],
                SiteSecret = ConfigurationManager.AppSettings["Quicklogons.SiteSecret"],
                HashAlgorithm = ConfigurationManager.AppSettings["Quicklogons.HashAlgorithm"],
                TimestampWindow = TimeSpan.FromSeconds(v)
            };
        }
    }
}
