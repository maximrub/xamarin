using Exrin.Abstraction;
using Friends.Domain.Peoples.Entities;

namespace Friends.ModelState
{
    public interface IMainModelState : IModelState
    {
        Person SelectedPerson { get; set; }
    }
}
