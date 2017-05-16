using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppSecretManager;
using AppSecretManager.Core.Models.Secrets;

namespace AppSecretManager.Model
{
    public interface IDataService 
    {
        void GetAllSecrets(Action<IList<IApiSecret>, Exception> callback);
        void GetApiSecret(Action<IApiSecret, Exception> callback, string secret);
        void InsertApiSecret(Action<bool> callback, IApiSecret secret);
        void UpdateApiSecret(Action<bool> callback, IApiSecret secret);

        //void GetComputerSecrets(Action<ComputerAPI, Exception> callback);

    }
}
