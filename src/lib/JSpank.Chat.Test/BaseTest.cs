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

        protected string Lorem(int length = 20)
        {
            var value = "Curabitur hendrerit iaculis aliquam. Donec sit amet erat urna. Maecenas pretium quam in tincidunt aliquam. Curabitur mollis auctor pellentesque. Donec faucibus ipsum sit amet massa imperdiet fermentum. Aliquam interdum, diam quis rutrum cursus, libero sapien sodales sapien, vel rhoncus est lacus vel dui. Integer dapibus vitae nunc eget interdum. Mauris ut sapien at lorem suscipit egestas lacinia eu eros. Fusce felis est, feugiat ac erat ac, pellentesque vestibulum diam. Interdum et malesuada fames ac ante ipsum primis in faucibus. Vestibulum ullamcorper tortor dictum, vestibulum sapien at, feugiat nulla. Sed sodales est at enim eleifend euismod. Donec sed sapien imperdiet metus semper luctus. Mauris ac tincidunt diam, id hendrerit orci.";
            return value.Substring(0, (length > value.Length ? value.Length : length));
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

