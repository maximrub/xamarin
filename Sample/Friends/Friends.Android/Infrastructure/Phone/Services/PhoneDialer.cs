using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.Telephony;
using Friends.Domain.Phone.Interfaces;
using Xamarin.Forms;

namespace Friends.Droid.Infrastructure.Phone.Services
{
    public class PhoneDialer : IPhoneDialer
    {
        public bool Dial(string i_Number)
        {
            Context context = Forms.Context;

            if (context == null)
            {
                return false;
            }

            Intent intent = new Intent(Intent.ActionCall);
            intent.SetData(Uri.Parse("tel:" + i_Number));

            if (IsIntentAvailable(context, intent))
            {
                context.StartActivity(intent);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if an intent can be handled.
        /// </summary>
        private bool IsIntentAvailable(Context i_Context, Intent i_Intent)
        {
            PackageManager packageManager = i_Context.PackageManager;

            IEnumerable<ResolveInfo> list =
                packageManager.QueryIntentServices(i_Intent, 0).Union(packageManager.QueryIntentActivities(i_Intent, 0));
            if (list.Any())
            {
                return true;
            }

            TelephonyManager telephonyManager = TelephonyManager.FromContext(i_Context);

            return telephonyManager.PhoneType != PhoneType.None;
        }
    }
}