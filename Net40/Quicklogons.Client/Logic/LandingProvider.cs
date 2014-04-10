using Quicklogons.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client.Logic
{
    public class LandingProvider : ILandingProvider
    {
        ISettingsProvider _settingProvider;

        public LandingProvider(ISettingsProvider settingProvider)
        {
            _settingProvider = settingProvider;
        }

        public ILandingResponse LandOn(IDictionary<string, string> parameters)
        {
            var rq = new LandingRequest 
            {
                Provider = GetParam(parameters, "provider"),
                UserId = GetParam(parameters, "userid"),
                Name = GetParam(parameters, "name"),
                Email = GetParam(parameters, "email"),
                ReturnUrl = GetParam(parameters, "returnurl"),
                Timestamp = GetParam(parameters, "timestamp"),
                Hash = GetParam(parameters, "hash"),
                Error = GetParam(parameters, "error")
            };

            var settings = _settingProvider.GetSettings();

            var hash = !string.IsNullOrWhiteSpace(rq.Hash) ? CaluclateHash(rq, settings) : null;
            if (hash == null || !string.Equals(rq.Hash, hash, StringComparison.InvariantCulture))
            {
                return new LandingResponse { Error = "Hash is incorect" };
            }

            DateTime ts = DateTime.TryParse(rq.Timestamp, null, System.Globalization.DateTimeStyles.AdjustToUniversal, out ts) ? ts : DateTime.MinValue;
            if (DateTime.UtcNow - ts > settings.TimestampWindow)
            {
                return new LandingResponse { Error = "Request expired" };
            }

            if (!string.IsNullOrWhiteSpace(rq.Error))
            {
                return rq;
            }

            if (string.IsNullOrWhiteSpace(rq.UserId))
            {
                return new LandingResponse { Error = "Wrong UserId" };
            }

            return rq;
        }

        string CaluclateHash(ILandingRequest rq, ISettings settings)
        {
            var content2Hash = new StringBuilder(settings.SiteKey)
                                         .Append(rq.UserId)
                                         .Append(rq.Name)
                                         .Append(rq.Email)
                                         .Append(rq.Timestamp)
                                         .Append(rq.Provider)
                                         .Append(rq.ReturnUrl)
                                         .Append(rq.Error)
                                         .Append(settings.SiteSecret);

            var provider = System.Security.Cryptography.HMAC.Create(settings.HashAlgorithm);
            provider.Key = Encoding.UTF8.GetBytes(settings.SiteSecret);
            provider.Initialize();
            var binary = provider.ComputeHash(Encoding.UTF8.GetBytes(content2Hash.ToString()));
            return Convert.ToBase64String(binary);
        }

        string GetParam(IDictionary<string, string> parameters, string parameterName)
        {
            string r = parameters.TryGetValue(parameterName, out r) ? r : null;
            return r;
        }
    }
}
