using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.NancyFx
{
    public static class NancyModuleExtensions
    {
        public static string GetReturnUrl(this NancyModule module)
        {
            return module.Request.Query.ReturnUrl.HasValue ? module.Request.Query.ReturnUrl.Value as string : null; 
        }

        public static string ValidateLocalUrl(this NancyModule module, string absoluteOrRelativeUrl)
        {
            var baseUri = new Uri(module.Request.Url.SiteBase);
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
