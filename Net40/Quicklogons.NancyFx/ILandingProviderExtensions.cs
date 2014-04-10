using Nancy;
using Quicklogons.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.NancyFx
{
    public static class ILandingProviderExtensions
    {
        public static ILandingResponse LandOn(this ILandingProvider landingProvider,  Request rq)
        {
            var parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var pair in rq.Query as IDictionary<string, object>) parameters[pair.Key] = (pair.Value as dynamic).Value as string;
            if (rq.Method == "POST")
            {
                foreach (var pair in rq.Form as IDictionary<string, object>) parameters[pair.Key] = (pair.Value as dynamic).Value as string;
            }

            return landingProvider.LandOn(parameters);
        }
    }
}
