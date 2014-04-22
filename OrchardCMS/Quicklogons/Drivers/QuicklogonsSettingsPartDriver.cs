using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Orchard.Security;
using Quicklogons.Models;
using System.Web.Mvc;

namespace Quicklogons.Drivers
{
    [OrchardFeature("Quicklogons")]
    public class QuicklogonsSettingsPartDriver : ContentPartDriver<QuicklogonsSettingsPart>
    {
        private readonly IEncryptionService _encryptionService;

        public Localizer T { get; set; }

        public QuicklogonsSettingsPartDriver(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
            T = NullLocalizer.Instance;
        }

        protected override string Prefix { get { return "QuicklogonsSettings"; } }

        protected override DriverResult Editor(QuicklogonsSettingsPart part, dynamic shapeHelper)
        {

            return ContentShape("Parts_Quicklogons_SiteSettings",
                               () => shapeHelper.EditorTemplate(TemplateName: "Parts.Quicklogons.SiteSettings",
                                                                Model: part,
                                                                Prefix: Prefix
                                                                )).OnGroup("Quicklogons");
        }

        protected override DriverResult Editor(QuicklogonsSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            if(updater.TryUpdateModel(part, Prefix, null, null))
            {
                if(!string.IsNullOrWhiteSpace(part.SiteSecret))
                {
                    part.Record.EncryptedSiteSecret = Convert.ToBase64String(_encryptionService.Encode(Encoding.UTF8.GetBytes(part.SiteSecret)));
                }
            }
            return Editor(part, shapeHelper);
        }
    }
}
