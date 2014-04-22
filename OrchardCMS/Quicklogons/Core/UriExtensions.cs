using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quicklogons.Core
{
    public static class UriExtensions
    {
        public static string ValidateLocalUrl(this Uri siteUri, string absoluteOrRelativeUrl)
        {
            var baseUri = new Uri(siteUri, string.Empty);
            if (String.IsNullOrEmpty(absoluteOrRelativeUrl)) return baseUri.ToString();

            Uri uri;
            if (Uri.TryCreate(absoluteOrRelativeUrl, UriKind.Absolute, out uri) &&
                string.Equals(baseUri.Host, uri.Host, StringComparison.OrdinalIgnoreCase))
            {
                return uri.ToString();
            }
            else
            {
                bool isLocal = !absoluteOrRelativeUrl.StartsWith("http:", StringComparison.OrdinalIgnoreCase) &&
                               !absoluteOrRelativeUrl.StartsWith("https:", StringComparison.OrdinalIgnoreCase) &&
                               Uri.IsWellFormedUriString(absoluteOrRelativeUrl, UriKind.Relative);
                return isLocal ? absoluteOrRelativeUrl : baseUri.ToString();
            }
        }
    }
}