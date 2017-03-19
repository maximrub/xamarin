using BeaconApp.ViewModel;
using Exrin.Abstraction;
using Exrin.Framework;
using Exrin.Navigation.XamarinForms;
using Friends.Locator;
using Friends.ViewModel;
using Xamarin.Forms;
using LoginView = Friends.View.LoginView;

namespace Friends.Stacks
{
    public class AuthenticationStack : BaseStack
    {
        public AuthenticationStack(IViewService i_ViewService)
            : base(new NavigationProxy(new NavigationPage()), i_ViewService, eStack.Authentication)
        {
            ShowNavigationBar = false;
        }

        protected override void Map()
        {
            NavigationMap<LoginView, LoginViewModel>(nameof(Views.eAuthentication.Login));
        }

        public override string NavigationStartKey
        {
            get
            {
                return nameof(Views.eAuthentication.Login);
            }
        }
    }
}
