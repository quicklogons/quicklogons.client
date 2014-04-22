using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Quicklogons.Models;

namespace Quicklogons.Handlers
{
    [OrchardFeature("Quicklogons")]
    public class QuicklogonsSettingsPartHandler : ContentHandler
    {
        public Localizer T { get; set; }

        public QuicklogonsSettingsPartHandler(IRepository<QuicklogonsSettingsPartRecord> repository)
        {
            Filters.Add(new ActivatingFilter<QuicklogonsSettingsPart>("Site"));
            Filters.Add(StorageFilter.For(repository));
            T = NullLocalizer.Instance;
        }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Quicklogons")) { Id = "Quicklogons" });
        }
    }
}
