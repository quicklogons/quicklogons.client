using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;
using Nancy.Responses;
using Quicklogons.Client;
using Quicklogons.NancyFx;
using Sample.NancyFx.Website.Core;
using Sample.NancyFx.Website.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample.NancyFx.Website.Models;

namespace Sample.NancyFx.Website.Modules
{
    public class AccountModule : NancyModule
    {
        ILoginProvider _loginProvider;
        ILandingProvider _landingProvider;
        IUserService _userService;

        public AccountModule(ILoginProvider loginLinksProvider, ILandingProvider landingProvider, IUserService userService)
        {
            _loginProvider = loginLinksProvider;
            _landingProvider = landingProvider;
            _userService = userService;

            Get["/Login"] = Login;
            Get["/LoggedIn"] = LoggedIn;
            Get["/Logout"] = LogOut;
            Get["/Profile"] = Profile;
        }

        dynamic Login(dynamic p)
        {
            var returnUrl = this.GetReturnUrl();
            var m = new AccountLoginModel 
            { 
                Title = "Login",
                LoginLinks = _loginProvider.GetLoginLinks(returnUrl)
            };
            return View["Login", m];
        }

        dynamic LoggedIn(dynamic p)
        {
            var response = _landingProvider.LandOn(this.Request);
            if (string.IsNullOrWhiteSpace(response.Error))
            {
                var user = _userService.GetUser(response);
                var returnUrl = this.ValidateLocalUrl(response.ReturnUrl);
                return FormsAuthentication.UserLoggedInRedirectResponse(Context, user.UserId, DateTime.Now.AddDays(1), returnUrl);
            }
            return View["Error", new ErrorModel { Title = "Login failed", Message = string.Format("Reason: {0}", response.Error) }];
        }

        dynamic LogOut(dynamic p)
        {
            var returnUrl = this.GetReturnUrl(); 
            return this.Logout(this.ValidateLocalUrl(returnUrl));
        }

        dynamic Profile(dynamic p)
        {
            this.RequiresAuthentication();
            var user = _userService.GetUserByIdentity(Context.CurrentUser);
            var m = new AccountProfileModel 
            { 
                Title = "Profile",
                UserLogin = user.UserLogin,
                UserName = user.UserName,
                UserEmail = user.UserEmail
            };
            return View["Profile", m];
        }
    }
}