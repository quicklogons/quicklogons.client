using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client
{
    public interface ISettings
    {
        string Url { get; }
        string SecureUrl { get; }
        string SiteKey { get; }
        string SiteSecret { get; }
        string HashAlgorithm { get; }
        TimeSpan TimestampWindow { get; }
    }
}
