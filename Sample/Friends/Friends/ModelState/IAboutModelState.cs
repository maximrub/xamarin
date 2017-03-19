using Exrin.Abstraction;
using Friends.Domain.Peoples.Entities;

namespace Friends.ModelState
{
    public interface IAboutModelState : IModelState
    {
        Person SelectedPerson { get; set; }
    }
}
