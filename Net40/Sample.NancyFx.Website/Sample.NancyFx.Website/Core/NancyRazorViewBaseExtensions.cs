using Nancy.ViewEngines;
using Nancy.ViewEngines.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace Sample.NancyFx.Website.Core
{
    public static class NancyRazorViewBaseExtensions
    {
        static string _version = Assembly.GetAssembly(typeof(NancyRazorViewBaseExtensions)).GetName().Version.ToString();

        public static string GetVersion<T>(this NancyRazorViewBase<T> view) { return _version; }

        public static string CreateReturnUrl<T>(this NancyRazorViewBase<T> view, IRenderContext renderContext)
        {
            var url = renderContext.Context.Request.Url.Path;
            var query = renderContext.Context.Request.Url.Query;
            if (url != null && url.EndsWith("/login", StringComparison.OrdinalIgnoreCase)) { url = string.Empty; query = string.Empty; }
            return view.Url.Content(url + query);
        }

        public static string UrlEncode<T>(this NancyRazorViewBase<T> view, string content)
        {
            return WebUtility.UrlEncode(content);
        }

        public static string[] CalculateLayout<T>(this NancyRazorViewBase<T> view, string leftAside, string content, string rightAside)
        {
            var sections = new string[3];
            leftAside = view.IsSectionDefined(leftAside) ? leftAside : null;
            rightAside = view.IsSectionDefined(rightAside) ? rightAside : null;
            if (leftAside == null && rightAside == null)
            {
                sections[1] = "col-md-12";
            }
            else if (rightAside != null && leftAside == null)
            {
                sections[1] = "col-md-9";
                sections[2] = "col-md-3";
            }
            else if (rightAside == null && leftAside != null)
            {
                sections[0] = "col-md-3";
                sections[1] = "col-md-9";
            }
            else
            {
                sections[0] = "col-md-2";
                sections[1] = "col-md-8";
                sections[2] = "col-md-2";
            }
            return sections;
        }
    }
}