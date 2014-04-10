using Nancy;
using Nancy.Authentication.Forms;
using Quicklogons.Client;
using Quicklogons.Client.Logic;
using Sample.NancyFx.Website.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.NancyFx.Website
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            container.Register<ISettingsProvider, WebSettingsProvider>();
            container.Register<ILoginProvider, LoginProvider>();
            container.Register<ILandingProvider, LandingProvider>();

            container.Register<IUserService, UserService>();
            container.Register<IUserMapper>(container.Resolve<IUserService>());
        }

        protected override void RequestStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            FormsAuthentication.Enable(pipelines, new FormsAuthenticationConfiguration { RedirectUrl = "/login", UserMapper = container.Resolve<IUserMapper>() });
        }
    }
}