using Autofac;
using Orchard;
using Orchard.Security;
using Quicklogons.Client;
using Quicklogons.Client.Logic;
using Quicklogons.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quicklogons
{
    public class QuicklogonsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register<ISettingsProvider>(ctx => new ContentPartRecordSettingsProvider(   
                                                                                                ctx.Resolve<IWorkContextAccessor>(), 
                                                                                                ctx.Resolve<IEncryptionService>())
                                                                                            );
            builder.Register<ILoginProvider>(ctx => new LoginProvider(ctx.Resolve<ISettingsProvider>()));
            builder.Register<ILandingProvider>(ctx => new LandingProvider(ctx.Resolve<ISettingsProvider>()));
        }
    }
}