using Orchard.ContentManagement;
using Orchard.Mvc.Extensions;
using Orchard.Environment.Extensions;
using Orchard.Localization;
using Quicklogons.Client;
using Quicklogons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard;
using Orchard.Security;
using Orchard.Logging;
using Orchard.Users.Models;
using Orchard.UI.Notify;

namespace Quicklogons.Controllers
{
    [HandleError]
    [OrchardFeature("Quicklogons")]
    public class LandingController : Controller
    {
        private readonly ILandingProvider _landingProvider;
        private readonly IOrchardServices _orchardServices;
        private readonly IMembershipService _membershipService;
        private readonly IAuthenticationService _authenticationService;

        public ILogger Logger { get; set; }
        public Localizer T { get; set; }

        public LandingController(ILandingProvider landingProvider,
                                 IMembershipService membershipService,
                                 IAuthenticationService authenticationService,
                                 IOrchardServices orchardServices)
        {
            _landingProvider = landingProvider;
            _membershipService = membershipService;
            _authenticationService = authenticationService;
            _orchardServices = orchardServices;

            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        public ActionResult LoggedOn(LandingLoggedOnModel m)
        {
            var currentUser = _authenticationService.GetAuthenticatedUser();
            if (currentUser != null) _authenticationService.SignOut();

            var response = _landingProvider.LandOn(m.ToDictionary());

            if (string.IsNullOrWhiteSpace(response.Error))
            {
                var userName = string.Format("{0}@{1}", response.UserId, response.Provider);
                var user = _membershipService.GetUser(userName);
                
                user = user == null ? _membershipService.CreateUser(new CreateUserParams(userName, Guid.NewGuid().ToString(), response.Email, null, null, true)) as UserPart : user;

                if (user == null)
                {
                    _orchardServices.Notifier.Add(NotifyType.Error, T("User can not be created and assigned from {0} credentials", response.Provider));
                }
                else
                {
                    var userPart = user.As<UserPart>();
                    if (userPart.RegistrationStatus != UserStatus.Approved)
                    {
                        _orchardServices.Notifier.Add(NotifyType.Error, T("User was disabled by site administrator"));
                    }
                    else
                    {
                        if (!string.Equals(userPart.Email, response.Email, StringComparison.OrdinalIgnoreCase))
                        {
                            userPart.Email = response.Email;
                            userPart.EmailStatus = UserStatus.Pending;
                        }
                        var userProfilePart = user.As<UserProfilePart>();
                        if (!string.Equals(userProfilePart.Name, response.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            userProfilePart.Name = response.Name;
                        }
                        _authenticationService.SignIn(user, false);
                    }
                }
            }
            else
            {
                _orchardServices.Notifier.Add(NotifyType.Error, T(response.Error));
            }
            return this.RedirectLocal(string.IsNullOrWhiteSpace(response.ReturnUrl) ? "~/" : response.ReturnUrl);
        }
    }
}