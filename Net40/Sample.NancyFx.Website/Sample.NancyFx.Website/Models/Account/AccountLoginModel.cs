using Quicklogons.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.NancyFx.Website.Models.Account
{
    public class AccountLoginModel : TitleModel
    {
        public IEnumerable<ILoginLink> LoginLinks { get; set; }
    }
}