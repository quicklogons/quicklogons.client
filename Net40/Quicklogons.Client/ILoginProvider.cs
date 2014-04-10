using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client
{
    public interface ILoginProvider
    {
        string GetLoginPageLink(string returnUrl = null);
        IEnumerable<ILoginLink> GetLoginLinks(string returnUrl = null);
    }
}
