using System.Threading.Tasks;
using Exrin.Abstraction;
using Exrin.Base;
using Exrin.Framework;
using Friends.Domain.Peoples.Entities;
using Friends.Model;
using Friends.VisualState;

namespace Friends.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IAboutModel r_Model;

        public AboutViewModel(IAboutModel i_Model, IExrinContainer i_ExrinContainer)
            : base(i_ExrinContainer, new AboutVisualState(i_Model))
        {
            r_Model = i_Model;
        }

        public override async Task OnNavigated(object i_Args)
        {
            await base.OnNavigated(i_Args);
            ((AboutVisualState)VisualState).SelectedPerson = (Person)i_Args;
        }

        public IRelayCommand CallCommand
        {
            get
            {
                return GetCommand(() =>
                {
                    return Execution.ViewModelExecute(new CallOperation(r_Model));
                });
            }
        }
    }
}
