using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client
{
    public interface ILandingRequest : ILandingResponse
    {
        string Timestamp { get; }
        string Hash { get; }
    }
}
