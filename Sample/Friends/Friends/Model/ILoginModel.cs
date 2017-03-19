using System.Threading.Tasks;
using Exrin.Abstraction;

namespace Friends.Model
{
    public interface ILoginModel : IBaseModel
    {
        Task<bool> Login();
    }
}
