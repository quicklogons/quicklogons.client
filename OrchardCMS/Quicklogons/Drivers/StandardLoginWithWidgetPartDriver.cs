using Orchard;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Quicklogons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quicklogons.Drivers
{
    public class StandardLoginWithWidgetPartDriver : ContentPartDriver<StandardLoginWithWidgetPart>
    {
        IWorkContextAccessor _workContextAccessor;

        public StandardLoginWithWidgetPartDriver(IWorkContextAccessor workContextAccessor)
        {
            _workContextAccessor = workContextAccessor;
        }

        protected override DriverResult Display(StandardLoginWithWidgetPart part, string displayType, dynamic shapeHelper)
        {
            var wc = _workContextAccessor.GetContext();
            var sp = wc.CurrentSite.As<QuicklogonsSettingsPart>();
            var returnUrl = wc.HttpContext.Request.Url.ToString();
            var url = new Uri(new Uri(sp.SecureUrl), string.Format("/Login/Site/{0}?returnUrl={1}", sp.SiteKey, HttpUtility.UrlEncode(returnUrl))).ToString();
            return ContentShape("Parts_StandardLoginWithWidget", () => shapeHelper.Parts_StandardLoginWithWidget(Url: url));
        }
    }
}