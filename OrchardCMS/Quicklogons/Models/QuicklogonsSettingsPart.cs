using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quicklogons.Models
{
    [OrchardFeature("Quicklogons")]
    public class QuicklogonsSettingsPart : ContentPart<QuicklogonsSettingsPartRecord>
    {
        //[Required(ErrorMessage = "Url is required")]
        public string Url { get { return Record.Url; } set { Record.Url = value; } }

        //[Required(ErrorMessage = "Secure url is required")]
        public string SecureUrl { get { return Record.SecureUrl; } set { Record.SecureUrl = value; } }

        [Required(ErrorMessage = "Site key is required")]
        public string SiteKey { get { return Record.SiteKey; } set { Record.SiteKey = value; } }

        public string SiteSecret { get; set; }

        [Required(ErrorMessage = "Hash algorithm is required")]
        public string HashAlgorithm { get { return Record.HashAlgorithm; } set { Record.HashAlgorithm = value; } }
    }
}