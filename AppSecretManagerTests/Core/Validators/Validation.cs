using System;
using System.Collections.Generic;
using AppSecretManager.Core.Models.API;
using AppSecretManager.Core.Models.Secrets;
using AppSecretManager.Core.Util;
using AppSecretManager.Core.Util.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Assert = NUnit.Framework.Assert;

namespace AppSecretManagerTests.Core.Validators
{
    [TestFixture()]
    public class UnitTest1
    {
        public IApiSecret ApiSecretFaulted { get; set; } = new ApiSecret()
        {
            AllowedApis = new List<ApiType>() { ApiType.ComputerApi, ApiType.Telefoni },
            Description = "Testing description",
            Owner = "",
            Secret = SecretGenerator.RandomString(15)
        };

        public IApiSecret ApiSecretCorrect { get; set; } = new ApiSecret()
        {
            AllowedApis = new List<ApiType>() { ApiType.ComputerApi, ApiType.Telefoni },
            Description = "Testing description",
            Owner = "sf9398",
            Secret = SecretGenerator.RandomString(15)
        };

        [Test]
        public void IApiSecretValidation()
        {
            var result = ValidationHandler.ValidateIApiSecret(ApiSecretFaulted);
            Assert.IsFalse(result.Item1);
            Assert.IsNotEmpty(result.Item2);

            var result2 = ValidationHandler.ValidateIApiSecret(ApiSecretCorrect);
            Assert.IsTrue(result2.Item1);
            Assert.IsEmpty(result2.Item2);
        }
    }
}
