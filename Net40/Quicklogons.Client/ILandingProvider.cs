using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client
{
    public interface ILandingProvider
    {
        ILandingResponse LandOn(IDictionary<string, string> parameters);
    }
}
