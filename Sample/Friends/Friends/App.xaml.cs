using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exrin.Abstraction;
using Exrin.Framework;
using Exrin.IOC.LightInjectServiceProvider;
using Friends.Locator;
using Xamarin.Forms;

namespace Friends
{
    public partial class App : Application
    {
        public App(PlatformInitializer i_PlatformInitializer)
        {
            InitializeComponent();
            Exrin.IOC.LightInjectServiceProvider.Bootstrapper.Init(i_PlatformInitializer, new FormsInitializer());
        }

        protected override void OnStart()
        {
            INavigationService navigationService = Exrin.IOC.LightInjectServiceProvider.Bootstrapper.Instance.InjectionProxy.Get<INavigationService>();
            navigationService.Navigate(new StackOptions()
            {
                StackChoice = eStack.Authentication
            });
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
