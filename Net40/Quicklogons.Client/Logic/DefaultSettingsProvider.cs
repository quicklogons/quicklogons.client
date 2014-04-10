using Quicklogons.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client.Logic
{
    public class DefaultSettingsProvider : ISettingsProvider
    {
        private ISettings _settings = null;

        public DefaultSettingsProvider(string url, string secureUrl, string siteKey, string siteSecret, string hashAlgoritm, TimeSpan timestampWindow)
        {
            _settings = new Settings 
            { 
                Url = url,
                SecureUrl = secureUrl,
                SiteKey = siteKey,
                SiteSecret = siteSecret,
                HashAlgorithm = hashAlgoritm,
                TimestampWindow = timestampWindow
            };
        }

        public ISettings GetSettings()
        {
            return _settings;
        }
    }
}
