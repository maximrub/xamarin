using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exrin.Abstraction;
using Exrin.Framework;
using Exrin.IOC;
using Friends.Locator;
using Xamarin.Forms;

namespace Friends
{
    public partial class App : Application
    {
        public App(PlatformInitializer i_PlatformInitializer)
        {
            InitializeComponent();
            Exrin.IOC.LightInjectProvider.Init(i_PlatformInitializer, new FormsInitializer());
            IInjectionProxy injectionProxy = Bootstrapper.Instance.Init();
            INavigationService navigationService = injectionProxy.Get<INavigationService>();
            navigationService.Navigate(new StackOptions()
            {
                StackChoice = eStack.Authentication
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
