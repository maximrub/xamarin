using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Friends.Domain.Validation.Interfaces;

namespace Friends.Domain.Validation.Services
{
    public class Validator : IValidator
    {
        public bool Validate<T>(T i_Obj, out ICollection<ValidationResult> o_ValidationResults) where T : IValidatable
        {
            const bool v_ValidateAllProperties = true;
            o_ValidationResults = new List<ValidationResult>();

            return System.ComponentModel.DataAnnotations.Validator.TryValidateObject(i_Obj, new ValidationContext(i_Obj), o_ValidationResults, v_ValidateAllProperties);
        }
    }
}