using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client.Data
{
    public class Settings : ISettings
    {
        public string Url { get; set; }
        public string SecureUrl { get; set; }
        public string SiteKey { get; set; }
        public string SiteSecret { get; set; }
        public string HashAlgorithm { get; set; }
        public TimeSpan TimestampWindow { get; set; }

        public Settings()
        {
            TimestampWindow = TimeSpan.FromSeconds(60);
        }
    }
}
