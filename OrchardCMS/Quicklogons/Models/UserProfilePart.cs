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
    public class UserProfilePart : ContentPart<UserProfilePartRecord>
    {
        public string Name { get { return Record.Name; } set { Record.Name = value; } }
    }
}