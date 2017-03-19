using Exrin.Abstraction;
using Exrin.Base;
using Exrin.Framework;
using Friends.Model;
using Friends.VisualState;

namespace Friends.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginModel r_Model;

        public LoginViewModel(ILoginModel i_Model, IExrinContainer i_ExrinContainer) :
           base(i_ExrinContainer, new LoginVisualState(i_Model))
        {
            r_Model = i_Model;
        }

        public IRelayCommand LoginCommand
        {
            get
            {
                return GetCommand(() =>
                {
                    return Execution.ViewModelExecute(new LoginOperation(r_Model));
                });
            }
        }

    }
}
