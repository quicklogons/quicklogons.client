using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client
{
    public interface ILoginLink
    {
        string ProviderName { get; }
        string Url { get; }
    }
}
