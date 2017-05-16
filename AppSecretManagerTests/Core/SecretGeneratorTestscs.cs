using System;
using System.Collections.Generic;
using System.Linq;
using AppSecretManager.Core.Util;
using NUnit.Framework;

//using TestContext = Microsoft.VisualStudio.TestTools.UnitTesting.TestContext;

namespace AppSecretManagerTests.Core
{
    /// <summary>
    ///     Summary description for SecretGeneratorTestscs
    /// </summary>
    [TestFixture]
    public class SecretGeneratorTestscs
    {
        //private TestContext testContextInstance;

        ///// <summary>
        /////Gets or sets the test context which provides
        /////information about and functionality for the current test run.
        /////</summary>
        //public TestContext TestContext
        //{
        //    get
        //    {
        //        return testContextInstance;
        //    }
        //    set
        //    {
        //        testContextInstance = value;
        //    }
        //}


        [Test]
        public void Secret_Length_Test()
        {
            var secret = SecretGenerator.RandomString(15);
            Assert.IsNotNull(secret, "Secret is null after gbeeing generated");
            Assert.AreEqual(15, secret.Length, "Secret is not the length spesified in generation");
        }

        [Test]
        public void Secret_Negative_Length_Test()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => SecretGenerator.RandomString(-15));
        }

        [Test]
        public void Secret_Uniqe_Test()
        {
            var numberOfGenerations = 1000;
            var range = Enumerable.Range(0, numberOfGenerations);

            var secrets = range.Select(i => SecretGenerator.RandomString(15));

            var duplicats = secrets.Distinct();

            Assert.AreEqual(numberOfGenerations, duplicats.Count());
        }
    }
}