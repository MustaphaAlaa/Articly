using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloggie.Services.Helper
{
    internal class ModelValidate
    {
        public static void ModelValidation(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool IsValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            if (!IsValid)
            {
                throw new Exception(String.Join("\n", validationResults));
            }
        }
    }
}
