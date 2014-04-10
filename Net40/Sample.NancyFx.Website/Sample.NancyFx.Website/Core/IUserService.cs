using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using Quicklogons.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.NancyFx.Website.Core
{
    public interface IUserService : IUserMapper
    {
        IUser GetUser(ILandingResponse response);
        IUser GetUserByIdentity(IUserIdentity userIdentity);
        Guid GetUserIdFromLogin(string login);
    }
}
