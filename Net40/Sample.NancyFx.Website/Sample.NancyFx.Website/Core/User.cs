using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.NancyFx.Website.Core
{
    public class User : IUser
    {
        static readonly string[] DefaultClaims = new string[] { "Guest" };

        public Guid UserId { get; set; }
        public string UserLogin { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public IEnumerable<string> Claims { get { return DefaultClaims; } }
    }
}