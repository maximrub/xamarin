using System.Collections.Generic;
using System.Threading.Tasks;
using Exrin.Abstraction;
using Friends.Domain.Peoples.Entities;
using Friends.ModelState;

namespace Friends.Model
{
    public interface IMainModel : IBaseModel
    {
        IMainModelState State { get; }

        Task<IEnumerable<Person>> GetPersons();
    }
}
