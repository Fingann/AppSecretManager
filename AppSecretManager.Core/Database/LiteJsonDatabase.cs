using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AppSecretManager.Core.Models.Secrets;
using AppSecretManager.Core.Settings;
using LiteDB;


namespace AppSecretManager.Core.Database
{
    public class LiteJsonDatabase : IRepository<IApiSecret>
    {
        
        public string CollectionName { get; set; } 
        public string DatabaseName { get; set; }




        public LiteJsonDatabase()
        {
            DatabaseName = CoreSettings.Default.connectionString;
            CollectionName = "AppSecrets";


        }
        public LiteJsonDatabase(string database, string collectionName)
        {
           
            var test = CoreSettings.Default.connectionString;
            CollectionName =  collectionName;
            DatabaseName = CoreSettings.Default.connectionString; ;
        }

      



        public bool Insert(IApiSecret entity)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);

                var key = collection.Insert(entity);
                return key != null;
            }
        }

        public void Delete(IApiSecret entity)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);
                
                collection.Delete(entity.Secret);
            }
        }
        public void Delete(Expression<Func<IApiSecret, bool>> predicate)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);

                collection.Delete(predicate);
            }
        }

        public void Delete(string secret)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);

               collection.Delete(x => x.Secret == secret);
            }
        }

        public bool Update(IApiSecret entity)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);
               
                return collection.Update(entity.Secret,entity);
            }
        }
        public IApiSecret Find(IApiSecret appsecret)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);

                return collection.FindById(appsecret.Secret);
            }
        }

        public IApiSecret Find(Expression<Func<IApiSecret, bool>> predicate)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);

                return collection.FindOne(predicate);
            }
        }

        public IList<IApiSecret> FindSevral(Expression<Func<IApiSecret, bool>> predicate)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);

                return collection.Find(predicate).ToList();
            }
        }

        public IList<IApiSecret> GetAll()
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);
                var all = collection.FindAll().ToList();
                return  all;
            }
        }

        public IApiSecret GetBySecret(string secret)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);

               return collection.FindById(secret);
            }
        }

        public bool ValidateSecret(string secret)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var collection = db.GetCollection<IApiSecret>(CollectionName);
                var found = collection.FindById(secret);
                return found != null;
            }
        }
    }
}
