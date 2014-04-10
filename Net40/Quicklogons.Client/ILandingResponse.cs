using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client
{
    public interface ILandingResponse
    {
        string Provider { get; }
        string UserId { get; }
        string Name { get; }
        string Email { get; }
        string ReturnUrl { get; }
        string Error { get; }
    }
}
