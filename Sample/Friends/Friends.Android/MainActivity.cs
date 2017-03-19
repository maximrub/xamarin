using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace Friends.Droid
{
    [Activity(Label = "Friends", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle i_Bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(i_Bundle);
            global::Xamarin.Forms.Forms.Init(this, i_Bundle);
            Exrin.Framework.App.Init(new Exrin.Framework.PlatformOptions() { Platform = Device.OS.ToString() });
            LoadApplication(new App(new AndroidInitializer()));
        }
    }
}

