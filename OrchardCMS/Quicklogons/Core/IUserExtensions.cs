using Orchard.Security;
using Orchard.ContentManagement;
using Quicklogons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quicklogons.Core
{
    public static class IUserExtensions
    {
        public static string DisplayName(this IUser user)
        { 
            var userProfilePart = user.As<UserProfilePart>();
            if (userProfilePart != null && !string.IsNullOrWhiteSpace(userProfilePart.Name)) return userProfilePart.Name;
            if (!string.IsNullOrWhiteSpace(user.Email)) return user.Email;
            return user.UserName;
        }
    }
}