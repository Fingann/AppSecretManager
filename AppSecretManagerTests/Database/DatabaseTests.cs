using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using AppSecretManager.Core.Database;
using AppSecretManager.Core.Models.API;
using AppSecretManager.Core.Models.Secrets;
using AppSecretManager.Core.Util;
using Assert = NUnit.Framework.Assert;

namespace AppSecretManagerTests.Database
{
 


    [TestFixture()]
    public class DatabaseTests
    {
        public LiteJsonDatabase LiteJsonDatabase { get; set; }

        public IApiSecret ApiSecret { get; set; } = new ApiSecret()
        {
            AllowedApis = new List<ApiType>() { ApiType.ComputerApi, ApiType.Telefoni },
            Description = "Testing description",
            Owner = "sf9398",
            Secret = SecretGenerator.RandomString(15)
        };
        [TestFixtureSetUp]
        public void ctor()
        {
            LiteJsonDatabase = new LiteJsonDatabase("Test.db", "testCollection");
            LiteJsonDatabase.Delete(secret => true);
        }

        //[SetUp]
        //public void Init()
        //{
        //    LiteJsonDatabase = new LiteJsonDatabase("Test.db","testCollection");
            

        //}

        

        [TearDown]
        public void RunAfterAnyTests()
        {
            LiteJsonDatabase.Delete((secret => true));
        }

        [Test]
        public void Database_Insert_tests()
        {
            Assert.AreEqual(0, LiteJsonDatabase.GetAll().Count);
           
            LiteJsonDatabase.Insert(ApiSecret);
            var apisecret = LiteJsonDatabase.GetAll();
            Assert.NotNull(apisecret);
            Assert.AreEqual(1, apisecret.Count);

        }

        [Test]
        public void Database_Delete_tests()
        {
            Assert.AreEqual(0, LiteJsonDatabase.GetAll().Count);

            LiteJsonDatabase.Insert(ApiSecret);
            Assert.AreEqual(1, LiteJsonDatabase.GetAll().Count);
            LiteJsonDatabase.Delete(ApiSecret);
            Assert.AreEqual(0, LiteJsonDatabase.GetAll().Count);
        }

        [Test]
        public void Database_Update_tests()
        {
          

            LiteJsonDatabase.Insert(ApiSecret);
            Assert.AreEqual(1, LiteJsonDatabase.GetAll().Count);
            var updatedApiSecret = LiteJsonDatabase.Find(ApiSecret);
            updatedApiSecret.Owner = "potetmos";

            LiteJsonDatabase.Update(updatedApiSecret);
            Assert.AreEqual("potetmos", LiteJsonDatabase.GetBySecret(ApiSecret.Secret).Owner);
        }

        [Test]
        public void Database_Validatesecret_tests()
        {

            LiteJsonDatabase.Insert(ApiSecret);

            Assert.AreEqual(1, LiteJsonDatabase.GetAll().Count);
            Assert.IsTrue(LiteJsonDatabase.ValidateSecret(ApiSecret.Secret));
            Assert.IsFalse(LiteJsonDatabase.ValidateSecret("hau27541HFAK82541asfag"));
        }

       
    }
}
