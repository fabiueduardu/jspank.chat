using Microsoft.VisualStudio.TestTools.UnitTesting;
using JSpank.Chat.App.Interfaces.Services;
using System;
using System.Configuration;
using System.Linq;
using JSpank.Chat.App.Services;
using System.Collections.Generic;
using System.Threading;

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

        UserAppService _IUserAppService
        {
            get
            {
                return base.container.GetInstance<UserAppService>();
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

            result = this._IPostAppService.Post(this.ApiService, dbid, username, post);

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

            for (var i = 0; i < 100; i++)
                result = this._IPostAppService.Post(this.ApiService, dbid, username, string.Format("{0:0000}: {1}", i, post));

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

            for (var i = 0; i < 10; i++)
            {
                var username_add = base.Key5;

                result = this._IUserAppService.Add(this.ApiService, dbid, username, username_add);
                Assert.IsTrue(result.IsValid, result.Message);

                result = this._IPostAppService.Post(this.ApiService, dbid, username, string.Format("{0:0000}: {1}", i, post));

                Assert.IsTrue(result.IsValid, result.Message);
                Assert.AreNotEqual(result, Guid.Empty, result.Message);
                Console.WriteLine("IsValid: {0}, Message: {1} , DbID: {2}", result.IsValid, result.Message, result.DbId);

                for (var _if = 0; _if < new Random().Next(0, 10); _if++)
                {

                    result = this._IPostAppService.Post(this.ApiService, dbid, username_add, string.Format("{0:0000}: {1}", _if, post));

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
            string username_add = null;


            for (var i = 0; i < 10; i++)
            {
                username_add = base.Key5;
                result = this._IUserAppService.Add(this.ApiService, dbid, username, username_add);
                Assert.IsTrue(result.IsValid, result.Message);

                result = this._IPostAppService.Post(this.ApiService, dbid, username, string.Format("{0:0000}: {1}", i, post));

                Assert.IsTrue(result.IsValid, result.Message);
                Assert.AreNotEqual(result, Guid.Empty, result.Message);
                Console.WriteLine("IsValid: {0}, Message: {1} , DbID: {2}", result.IsValid, result.Message, result.DbId);

                for (var a = 0; a < 100; a++)
                {

                    result = this._IPostAppService.Post(this.ApiService, dbid, username_add, string.Format("{0:0000}: {1}", a, post));

                    Assert.IsTrue(result.IsValid, result.Message);
                    Assert.AreNotEqual(result, Guid.Empty, result.Message);
                    Console.WriteLine("IsValid: {0}, Message: {1} , DbID: {2}", result.IsValid, result.Message, result.DbId);
                }
            }

        }

        [TestMethod]
        public void TargetDbId_ChatSimulate()
        {
            var DbId = ConfigurationManager.AppSettings["DbId"] as string;
            var DbIdUserName = ConfigurationManager.AppSettings["DbIdUserName"] as string;

            Assert.IsNotNull(DbId, "not dbid on settings");
            Assert.IsNotNull(DbIdUserName, "not username on settings");

            var rdn = new Random();
            var users = new List<string>();
            users.Add(DbIdUserName);
            var dbid = Guid.Parse(DbId);

            int index = 0;
            while (true)
            {
                var result = this._IPostAppService.Post(this.ApiService, dbid, users.OrderBy(a => Guid.NewGuid()).First(), base.Lorem(rdn.Next(1, 200)));
                Assert.IsTrue(result.IsValid, result.Message);

                if (index % 2 == 0)
                {
                    var usernameNew = base.Key5;
                    users.Add(usernameNew);

                    result = this._IUserAppService.Add(this.ApiService, dbid, users.First(), usernameNew);
                    Assert.IsTrue(result.IsValid, result.Message);

                    result = this._IPostAppService.Post(this.ApiService, dbid, usernameNew, base.Lorem(rdn.Next(1, 200)));
                    Assert.IsTrue(result.IsValid, result.Message);
                }

                Console.WriteLine("Index: {0}, Sleep {1:dd/MM/yyyy HH:mm:ss}", index, DateTime.Now);
                Thread.Sleep(rdn.Next(1, 5) * 1000);
                index++;
            }

        }
    }
}
