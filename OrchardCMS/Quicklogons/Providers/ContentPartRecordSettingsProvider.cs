using Orchard;
using Orchard.ContentManagement;
using Orchard.Security;
using Quicklogons.Client;
using Quicklogons.Client.Data;
using Quicklogons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Quicklogons.Providers
{
    public class ContentPartRecordSettingsProvider : ISettingsProvider
    {
        const string QuicklogonsUrl = "http://www.quicklogons.com/";
        const string QuicklogonsSecureUrl = "https://www.quicklogons.com/";

        IWorkContextAccessor _workContextAccessor;
        IEncryptionService _encryptionService;

        public ContentPartRecordSettingsProvider(IWorkContextAccessor workContextAccessor, IEncryptionService encryptionService)
        {
            _workContextAccessor = workContextAccessor;
            _encryptionService = encryptionService;
        }

        public ISettings GetSettings()
        {
            var wc = _workContextAccessor.GetContext();
            var sp = wc.CurrentSite.As<QuicklogonsSettingsPart>();
            string siteSecret = string.Empty;
            try
            {
                var encodedSiteSecret = sp.Record.EncryptedSiteSecret;
                siteSecret = string.IsNullOrWhiteSpace(encodedSiteSecret) ? string.Empty : Encoding.UTF8.GetString(_encryptionService.Decode(Convert.FromBase64String(encodedSiteSecret)));
            }
            catch { }

            return new Settings 
            { 
                Url = string.IsNullOrWhiteSpace(sp.Url) ? QuicklogonsUrl : sp.Url,
                SecureUrl = string.IsNullOrWhiteSpace(sp.SecureUrl) ? QuicklogonsSecureUrl : sp.SecureUrl,
                SiteKey = sp.SiteKey,
                SiteSecret = siteSecret,
                HashAlgorithm = sp.HashAlgorithm
            };
        }
    }
}