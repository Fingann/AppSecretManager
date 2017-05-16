using System;
using System.Collections.Generic;
using AppSecretManager.Core.Models.API;
using AppSecretManager.Core.Models.Secrets;
using AppSecretManager.Core.Util;
using AppSecretManager.Model;


namespace AppSecretManager.Design
{
    public class DesignDataService : IDataService
    {
        //public void GetData(Action<AppSecret, Exception> callback)
        //{
        //    // Use this to create design time data

        //    var item = new AppSecret();
        //    callback(item, null);
        //}
        public void GetAllSecrets(Action<IList<IApiSecret>, Exception> callback)
        {
            callback(new List<IApiSecret>(){new ApiSecret()
                {
                    AllowedApis = new List<ApiType>() { ApiType.ComputerApi, ApiType.Telefoni },
                    Description = "Testing description",
                    Owner = "sf9398",
                    Secret = SecretGenerator.RandomString(15)
                },new ApiSecret()
                {
                    AllowedApis = new List<ApiType>() { ApiType.ComputerApi, ApiType.Telefoni },
                    Description = "Testing description",
                    Owner = "sf9398",
                    Secret = SecretGenerator.RandomString(15)
                },new ApiSecret()
                {
                    AllowedApis = new List<ApiType>() { ApiType.ComputerApi, ApiType.Telefoni },
                    Description = "Testing description",
                    Owner = "sf9398",
                    Secret = SecretGenerator.RandomString(15)
                }
        }, null);
        }

        public void GetApiSecret(Action<IApiSecret, Exception> callback, string secret)
        {
            throw new NotImplementedException();
        }

        public void InsertApiSecret(Action<bool> callback, IApiSecret secret)
        {
            throw new NotImplementedException();
        }

        public void UpdateApiSecret(Action<bool> callback, IApiSecret secret)
        {
            throw new NotImplementedException();
        }
    }
}