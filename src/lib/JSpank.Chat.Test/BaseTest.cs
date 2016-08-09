using JSpank.Chat.App.AutoMapper;
using JSpank.Chat.CrossCutting.DI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System;

namespace JSpank.Chat.Test
{
    [TestClass]
    public class BaseTest
    {
        static Container _container;
        protected Container container
        {
            get { return _container; }
            set { _container = value; }
        }

        protected string Key5
        {
            get
            {
                return this.Key(05);
            }
        }

        protected string Key(int length = 5)
        {
            return Guid.NewGuid().ToString().Substring(0, length);
        }

        [TestInitialize]
        public void TestSetup()
        {
            this.container = new Container();
            DiContainer.RegisterAll(this.container);
            AutoMapperConfig.RegisterMappings();
        }

    }
}

