using Orchard.Environment.Extensions;
using Orchard.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Quicklogons
{
    [OrchardFeature("Quicklogons")]
    public class Routes : IRouteProvider
    {
        public void GetRoutes(ICollection<RouteDescriptor> routes)
        {
            foreach (var route in GetRoutes()) routes.Add(route);
        }

        public IEnumerable<RouteDescriptor> GetRoutes()
        {
            return new RouteDescriptor[] 
            {
                new RouteDescriptor
                {
                    Priority = 10,
                    Route = new Route(
                        "Landing/LoggedOn",
                        new RouteValueDictionary { { "area", "Quicklogons" }, { "controller", "Landing" }, { "action", "LoggedOn" }, },
                        new RouteValueDictionary(),
                        new RouteValueDictionary { { "area", "Quicklogons" } },
                        new MvcRouteHandler())
                },
                new RouteDescriptor
                {
                    Priority = 10,
                    Route = new Route(
                        "Profile/Edit",
                        new RouteValueDictionary { { "area", "Quicklogons" }, { "controller", "Profile" }, { "action", "Edit" }, },
                        new RouteValueDictionary(),
                        new RouteValueDictionary { { "area", "Quicklogons" } },
                        new MvcRouteHandler())
                },
                new RouteDescriptor
                {
                    Priority = 10,
                    Route = new Route(
                        "Profile",
                        new RouteValueDictionary { { "area", "Quicklogons" }, { "controller", "Profile" }, { "action", "Index" }, },
                        new RouteValueDictionary(),
                        new RouteValueDictionary { { "area", "Quicklogons" } },
                        new MvcRouteHandler())
                }
            };
        }
    }
}