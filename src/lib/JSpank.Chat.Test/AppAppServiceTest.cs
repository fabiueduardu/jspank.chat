using Microsoft.VisualStudio.TestTools.UnitTesting;
using JSpank.Chat.App.Interfaces.Services;
using System;
using System.Configuration;
using System.Linq;

namespace JSpank.Chat.Test
{
    [TestClass]
    public class AppAppServiceTest : BaseTest
    {
        IAppAppService _IAppAppService
        {
            get
            {
                return base.container.GetInstance<IAppAppService>();
            }
        }

        IPostAppService _IPostAppService
        {
            get
            {
                return base.container.GetInstance<IPostAppService>();
            }
        }

        string ApiService
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiService"] as string;
            }
        }

        [TestMethod]
        public void New()
        {
            var username = base.Key5;
            var result = this._IAppAppService.New(this.ApiService, username);

            Assert.IsTrue(result.IsValid, result.Message);
            Assert.AreNotEqual(result, Guid.Empty, result.Message);
            Console.WriteLine("IsValid: {0}, Message: {1} , DbID: {2}", result.IsValid, result.Message, result.DbId);
        }

        [TestMethod]
        public void New_With_HelloWorld()
        {
            var username = base.Key5;
            var result = this._IAppAppService.New(this.ApiService, username);

            Assert.IsTrue(result.IsValid, result.Message);
            Assert.AreNotEqual(result, Guid.Empty, result.Message);

            Guid dbid = result.DbId;
            string post = base.Key(10);
            string[] username_add = null;
            string[] username_remove = null;

            result = this._IPostAppService.Post(this.ApiService, dbid, username, post, username_add, username_remove);

            Assert.IsTrue(result.IsValid, result.Message);
            Assert.AreNotEqual(result, Guid.Empty, result.Message);
            Console.WriteLine("IsValid: {0}, Message: {1} , DbID: {2}", result.IsValid, result.Message, result.DbId);
        }

        [TestMethod]
        public void New_With_100_Posts()
        {
            var username = base.Key5;
            var result = this._IAppAppService.New(this.ApiService, username);

            Assert.IsTrue(result.IsValid, result.Message);
            Assert.AreNotEqual(result, Guid.Empty, result.Message);

            Guid dbid = result.DbId;
            string post = base.Key(10);
            string[] username_add = null;
            string[] username_remove = null;

            for (var i = 0; i < 100; i++)
                result = this._IPostAppService.Post(this.ApiService, dbid, username, string.Format("{0:0000}: {1}", i, post), username_add, username_remove);

            Assert.IsTrue(result.IsValid, result.Message);
            Assert.AreNotEqual(result, Guid.Empty, result.Message);
            Console.WriteLine("IsValid: {0}, Message: {1} , DbID: {2}", result.IsValid, result.Message, result.DbId);
        }

        [TestMethod]
        public void New_With_10_Friends_And_Randon_0_To_10_Posts()
        {
            var username = base.Key5;
            var result = this._IAppAppService.New(this.ApiService, username);

            Assert.IsTrue(result.IsValid, result.Message);
            Assert.AreNotEqual(result, Guid.Empty, result.Message);

            Guid dbid = result.DbId;
            string post = base.Key(10);
            string[] username_add = null;
            string[] username_remove = null;

            for (var i = 0; i < 10; i++)
            {
                username_add = new string[] { base.Key5 };
                result = this._IPostAppService.Post(this.ApiService, dbid, username, string.Format("{0:0000}: {1}", i, post), username_add, username_remove);

                Assert.IsTrue(result.IsValid, result.Message);
                Assert.AreNotEqual(result, Guid.Empty, result.Message);
                Console.WriteLine("IsValid: {0}, Message: {1} , DbID: {2}", result.IsValid, result.Message, result.DbId);

                for (var _if = 0; _if < new Random().Next(0, 10); _if++)
                {

                    result = this._IPostAppService.Post(this.ApiService, dbid, username_add.First(), string.Format("{0:0000}: {1}", _if, post), null, null);

                    Assert.IsTrue(result.IsValid, result.Message);
                    Assert.AreNotEqual(result, Guid.Empty, result.Message);
                    Console.WriteLine("IsValid: {0}, Message: {1} , DbID: {2}", result.IsValid, result.Message, result.DbId);
                }
            }

        }

        [TestMethod]
        public void New_With_10_Friends_And_100_Posts()
        {
            var username = base.Key5;
            var result = this._IAppAppService.New(this.ApiService, username);

            Assert.IsTrue(result.IsValid, result.Message);
            Assert.AreNotEqual(result, Guid.Empty, result.Message);

            Guid dbid = result.DbId;
            string post = base.Key(10);
            string[] username_add = null;
            string[] username_remove = null;

            for (var i = 0; i < 10; i++)
            {
                username_add = new string[] { base.Key5 };
                result = this._IPostAppService.Post(this.ApiService, dbid, username, string.Format("{0:0000}: {1}", i, post), username_add, username_remove);

                Assert.IsTrue(result.IsValid, result.Message);
                Assert.AreNotEqual(result, Guid.Empty, result.Message);
                Console.WriteLine("IsValid: {0}, Message: {1} , DbID: {2}", result.IsValid, result.Message, result.DbId);

                for (var a = 0; a < 100; a++)
                {

                    result = this._IPostAppService.Post(this.ApiService, dbid, username_add.First(), string.Format("{0:0000}: {1}", a, post), null, null);

                    Assert.IsTrue(result.IsValid, result.Message);
                    Assert.AreNotEqual(result, Guid.Empty, result.Message);
                    Console.WriteLine("IsValid: {0}, Message: {1} , DbID: {2}", result.IsValid, result.Message, result.DbId);
                }
            }

        }
    }
}
