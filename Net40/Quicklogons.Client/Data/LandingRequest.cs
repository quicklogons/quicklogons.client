using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client.Data
{
    class LandingRequest : LandingResponse, ILandingRequest
    {
        public string Timestamp { get; set; }
        public string Hash { get; set; }
    }
}
