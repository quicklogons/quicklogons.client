using Nancy;
using Nancy.Security;
using Quicklogons.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.NancyFx.Website.Core
{
    public class UserService : IUserService
    {
        public IDictionary<Guid, IUser> Users = new Dictionary<Guid, IUser>();

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            IUser user = Users.TryGetValue(identifier, out user) ? user : null;
            return user;
        }

        public Guid GetUserIdFromLogin(string login)
        {
            return Users.Values.Cast<IUser>().Where(u => u.UserLogin == login).Select(u => u.UserId).FirstOrDefault();
        }

        public IUser GetUser(ILandingResponse response)
        {
            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserLogin = string.Format("{0}@{1}", response.UserId, response.Provider),
                UserName = response.Name,
                UserEmail = response.Email
            };
            Users[user.UserId] = user;
            return user;
        }

        public IUser GetUserByIdentity(IUserIdentity userIdentity)
        {
            return userIdentity as IUser;
        }
    }
}