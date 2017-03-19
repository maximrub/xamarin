using BeaconApp.ViewModel;
using Exrin.Abstraction;
using Exrin.Framework;
using Exrin.Navigation.XamarinForms;
using Friends.Locator;
using Friends.View;
using Friends.ViewModel;
using Xamarin.Forms;
using AboutView = Friends.View.AboutView;

namespace Friends.Stacks
{
    public class MainStack : BaseStack
    {

        public MainStack(IViewService i_ViewService)
            : base(new NavigationProxy(new NavigationPage()), i_ViewService, eStack.Main)
        {
            ShowNavigationBar = false;
        }

        protected override void Map()
        {
            NavigationMap<MainView, MainViewModel>(nameof(Views.eMain.Main));
            NavigationMap<AboutView, AboutViewModel>(nameof(Views.eMain.About));
        }

        public override string NavigationStartKey
        {
            get
            {
                return nameof(Views.eMain.Main);
            }
        }
    }
}
