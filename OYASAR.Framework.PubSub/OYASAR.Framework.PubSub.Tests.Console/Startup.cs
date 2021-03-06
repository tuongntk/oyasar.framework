﻿using System;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using OYASAR.Framework.PubSub.Core.Utils;
using OYASAR.Framework.PubSub.Server;
using OYASAR.Framework.PubSub.Tests.Console;
using Microsoft.Owin;
using Microsoft.Practices.ServiceLocation;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace OYASAR.Framework.PubSub.Tests.Console
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IWindsorContainer container = new WindsorContainer(new XmlInterpreter(AppDomain.CurrentDomain.BaseDirectory + "App_Config\\castle.config"));

            //ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
            
            var server = new ServerManager(Constants.HubName, Constants.DefaultServerProvider);

            server.Start(app);

            //var server2 = new ServerManager(new DefaultServerConfigurationProvider(connectionSecond, disconnectionSecond,
            //    Constants.HubNameTest, app, "/signalr"));

            //server2.Start();
        }
    }
}
