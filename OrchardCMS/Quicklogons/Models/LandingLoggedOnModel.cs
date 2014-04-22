using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quicklogons.Models
{
    public class LandingLoggedOnModel
    {
        public string Provider { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Timestamp { get; set; }
        public string Hash { get; set; }
        public string Error { get; set; }
        public string ReturnUrl { get; set; }

        public IDictionary<string, string> ToDictionary()
        {
            var parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            parameters["provider"] = Provider;
            parameters["userid"] = UserId;
            parameters["name"] = Name;
            parameters["email"] = Email;
            parameters["timestamp"] = Timestamp;
            parameters["hash"] = Hash;
            parameters["error"] = Error;
            parameters["returnurl"] = ReturnUrl;
            return parameters;
        }
    }
}