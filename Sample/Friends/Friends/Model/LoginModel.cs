using System.Threading.Tasks;
using Exrin.Abstraction;
using Exrin.Base;
using Friends.ModelState;

namespace Friends.Model
{
    public class LoginModel : BaseModel, ILoginModel
    {
        public LoginModel(IExrinContainer i_ExrinContainer)
            : base(i_ExrinContainer, new LoginModelState())
        {
        }

        public Task<bool> Login()
        {
            return Task.FromResult(true);
        }
    }
}