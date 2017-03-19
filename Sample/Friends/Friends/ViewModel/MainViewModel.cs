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
        public MainViewModel(IMainModel i_Model, IExrinContainer i_ExrinContainer)
            : base(i_ExrinContainer, new MainVisualState(i_Model))
        {
        }

        public IRelayCommand FullInfoCommand
        {
            get
            {
                return GetCommand(() =>
                {
                    return Execution.ViewModelExecute(new AboutOperation());
                });
            }
        }
    }
}