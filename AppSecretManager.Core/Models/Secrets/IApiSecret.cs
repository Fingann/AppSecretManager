using System.Collections.Generic;
using AppSecretManager.Core.Models.API;

namespace AppSecretManager.Core.Models.Secrets
{
    public interface IApiSecret
    {
        string Secret { get; set; }
        string Description { get; set; }
        string Owner { get; set; }
        List<ApiType> AllowedApis { get; set; }
    }
}