using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSecretManager.Core.Models.Secrets;
using AppSecretManager.Validators;
using FluentValidation.Results;

namespace AppSecretManager.Core.Util.Validators
{
    public static class ValidationHandler
    {
        public static Tuple<bool, string> ValidateIApiSecret(IApiSecret secret)
        {
            
            IApiSecretValidator validator = new IApiSecretValidator();
            ValidationResult results = validator.Validate(secret);
            bool validationSucceeded = results.IsValid;
            if (!validationSucceeded)
            {
                IList<ValidationFailure> failures = results.Errors;
                return new Tuple<bool, string>(validationSucceeded, failures.Select(x => x.ErrorMessage).Aggregate((a, b) => a + Environment.NewLine + b));
            }
            return new Tuple<bool, string>(true,String.Empty);
        }
    }
}
