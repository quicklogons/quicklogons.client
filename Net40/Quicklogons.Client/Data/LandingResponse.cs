using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client.Data
{
    class LandingResponse : ILandingResponse
    {
        public string Provider { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
        public string Error { get; set; }
    }
}
