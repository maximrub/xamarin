using System.Collections.Generic;
using System.Threading.Tasks;
using Exrin.Abstraction;
using Friends.Domain.Peoples.Entities;
using Friends.ModelState;

namespace Friends.Model
{
    public interface IAboutModel : IBaseModel
    {
        IAboutModelState State { get; }

        Task<bool> Call();
    }
}
