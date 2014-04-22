using Orchard.ContentManagement.Records;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quicklogons.Models
{
    [OrchardFeature("Quicklogons")]
    public class UserProfilePartRecord : ContentPartRecord
    {
        public virtual string Name { get; set; }
    }
}