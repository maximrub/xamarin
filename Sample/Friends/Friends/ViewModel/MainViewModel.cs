using BeaconApp.ViewModel;
using Exrin.Abstraction;
using Exrin.Base;
using Exrin.Framework;
using Friends.Model;
using Friends.VisualState;

namespace Friends.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IMainModel r_Model;

        public MainViewModel(IMainModel i_Model, IExrinContainer i_ExrinContainer)
            : base(i_ExrinContainer, new MainVisualState(i_Model))
        {
            r_Model = i_Model;
        }

        public IRelayCommand FullInfoCommand
        {
            get
            {
                return GetCommand(() =>
                {
                    return Execution.ViewModelExecute(new AboutOperation(), (i_Object) => r_Model.State.SelectedPerson != null);
                });
            }
        }
    }
}