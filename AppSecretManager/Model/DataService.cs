using System;
using System.Collections.Generic;
using AppSecretManager.Core.Database;
using AppSecretManager.Core.Models.Secrets;


namespace AppSecretManager.Model
{
    public class DataService : IDataService
    {
       public LiteJsonDatabase LiteJsonDatabase = new LiteJsonDatabase();

        public void GetAllSecrets(Action<IList<IApiSecret>, Exception> callback)
        {
            
            callback(LiteJsonDatabase.GetAll(), null);
            
        }

       

        public void InsertApiSecret(Action<bool> callback, IApiSecret secret)
        {
            callback(LiteJsonDatabase.Insert(secret));
        }

        public void UpdateApiSecret(Action<bool> callback, IApiSecret secret)
        {
            callback(LiteJsonDatabase.Update(secret));
        }

        public void GetApiSecret(Action<IApiSecret, Exception> callback, string secret)
        {
           
           callback(LiteJsonDatabase.GetBySecret(secret), null);
          
        }
    }
}