using System.Collections.Generic;
using AppSecretManager.Core.Models.API;
using LiteDB;
using PropertyChanged;

namespace AppSecretManager.Core.Models.Secrets
{
    [ImplementPropertyChanged]
    public class ApiSecret : IApiSecret
    {
        [BsonId]
        public string Secret { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public List<ApiType> AllowedApis { get; set; } =new List<ApiType>();
       
    }
}