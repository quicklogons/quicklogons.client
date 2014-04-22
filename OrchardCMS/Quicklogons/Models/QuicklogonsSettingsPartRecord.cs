using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quicklogons.Models
{
    [OrchardFeature("Quicklogons")]
    public class QuicklogonsSettingsPartRecord : ContentPartRecord
    {
        public virtual string Url { get; set; }
        public virtual string SecureUrl { get; set; }
        public virtual string SiteKey { get; set; }
        public virtual string EncryptedSiteSecret { get; set; }
        public virtual string HashAlgorithm { get; set; }
    }
}