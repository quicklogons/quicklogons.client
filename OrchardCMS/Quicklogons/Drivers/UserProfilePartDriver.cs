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
    public class UserProfilePartDriver : ContentPartDriver<UserProfilePart>
    {
        public Localizer T { get; set; }

        public UserProfilePartDriver()
        {
            T = NullLocalizer.Instance;
        }

        protected override string Prefix { get { return "UserProfile"; } }

        protected override DriverResult Display(UserProfilePart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_UserProfile",
                () => shapeHelper.Parts_UserProfile(Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(UserProfilePart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_UserProfileEdit",
                               () => shapeHelper.EditorTemplate(TemplateName: "Parts.UserProfile",
                                                                Model: part,
                                                                Prefix: Prefix
                                                                ));
        }

        protected override DriverResult Editor(UserProfilePart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}
