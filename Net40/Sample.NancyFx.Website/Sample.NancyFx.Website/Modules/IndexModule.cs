using Nancy;
using Quicklogons.Client;
using Sample.NancyFx.Website.Models.Index;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.NancyFx.Website.Modules
{
    public class IndexModule : NancyModule
    {
        ILoginProvider _loginLinksProvider;

        public IndexModule(ILoginProvider loginLinksProvider)
        {
            _loginLinksProvider = loginLinksProvider;

            Get["/"] = Index;
        }

        dynamic Index(dynamic p)
        {
            var m = new IndexHomeModel
            { 
                Title = string.Empty,
                LoginPageUrl = _loginLinksProvider.GetLoginPageLink(null)
            };
            return View["Home", m];
        }
    }
}