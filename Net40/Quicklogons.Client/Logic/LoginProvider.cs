using Quicklogons.Client.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Quicklogons.Client.Logic
{
    public class LoginProvider : ILoginProvider
    {
        const string ReturnUrlQuery = "?returnurl={0}";
        const string RelativeLoginPageUrl = "/login/site/{0}";
        const string RelativeRequestUrl = "/login/site/{0}.json";

        ISettingsProvider _settingProvider;

        public LoginProvider(ISettingsProvider settingProvider)
        {
            _settingProvider = settingProvider;
        }

        public IEnumerable<ILoginLink> GetLoginLinks(string returnUrl = null)
        {
            var settings = _settingProvider.GetSettings();
            var relativeUrl = string.Format(RelativeRequestUrl, settings.SiteKey) + (!string.IsNullOrWhiteSpace(returnUrl) ? string.Format(ReturnUrlQuery, HttpUtility.UrlEncode(returnUrl)) : string.Empty);
            var requestUrl = new Uri(new Uri(settings.SecureUrl), relativeUrl);
            var rq = WebRequest.Create(requestUrl);
            rq.Method = "GET";
            var rs = rq.GetResponse();
            using (var stream = rs.GetResponseStream())
            {
                var js = new DataContractJsonSerializer(typeof(JsonLoginLink[]));
                return js.ReadObject(stream) as JsonLoginLink[];

            }
        }

        public string GetLoginPageLink(string returnUrl = null)
        {
            var settings = _settingProvider.GetSettings();
            var relativeUrl = string.Format(RelativeLoginPageUrl, settings.SiteKey) + (!string.IsNullOrWhiteSpace(returnUrl) ? string.Format(ReturnUrlQuery, HttpUtility.UrlEncode(returnUrl)) : string.Empty);
            var loginPageUri = new Uri(new Uri(settings.SecureUrl), relativeUrl);
            return loginPageUri.ToString();
        }
    }
}
