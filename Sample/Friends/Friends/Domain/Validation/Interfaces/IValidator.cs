using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Friends.Domain.Validation.Interfaces
{
    public interface IValidator
    {
        bool Validate<T>(T i_Obj, out ICollection<ValidationResult> o_ValidationResults) where T : IValidatable;
    }
}