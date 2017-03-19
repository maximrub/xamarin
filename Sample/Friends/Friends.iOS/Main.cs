using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Friends.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        public static void Main(string[] i_Args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(i_Args, null, "AppDelegate");
        }
    }
}
