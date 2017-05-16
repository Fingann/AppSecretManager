using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSecretManager.Core.Models.API;
using AppSecretManager.Core.Models.Secrets;
using FluentValidation;

namespace AppSecretManager.Validators
{
    public class IApiSecretValidator : AbstractValidator<IApiSecret>
    {
        public IApiSecretValidator()
        {
            RuleFor(apiSecret => apiSecret.Secret).NotEmpty().WithMessage("You must generate a App Secret");
            RuleFor(apiSecret => apiSecret.Owner).NotEmpty().WithMessage("Please specify a Owner");
            
            RuleFor(apiSecret => apiSecret.AllowedApis).Must(NotBeEmpty).WithMessage("Must select atleast one api");
          
        }

        private bool NotBeEmpty(List<ApiType> arg)
        {
            return arg.Count > 0;
        }
    }
}
