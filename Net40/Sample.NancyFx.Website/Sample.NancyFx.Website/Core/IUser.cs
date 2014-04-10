using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.NancyFx.Website.Core
{
    public interface IUser : IUserIdentity
    {
        Guid UserId { get; }
        string UserLogin { get; }
        string UserEmail { get; }
    }
}
