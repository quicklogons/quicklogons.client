using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.NancyFx.Website.Models.Account
{
    public class AccountProfileModel : TitleModel
    {
        public string UserLogin { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}