using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSecretManager.Core.Models.API;
using AppSecretManager.Core.Models.Secrets;

namespace AppSecretManager.Core.Models.Secrets
{
    public class InvalidApiSecret: IApiSecret
    {
        public string Secret { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public List<ApiType> AllowedApis { get; set; }
        
    }
}
