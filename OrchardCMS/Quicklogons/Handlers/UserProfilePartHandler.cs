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
    public class UserProfilePartHandler : ContentHandler
    {
        public UserProfilePartHandler(IRepository<UserProfilePartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}
