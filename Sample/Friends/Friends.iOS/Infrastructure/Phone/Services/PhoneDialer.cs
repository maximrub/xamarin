using Foundation;
using Friends.Domain.Phone.Interfaces;
using UIKit;

namespace Friends.iOS.Infrastructure.Phone.Services
{
    public class PhoneDialer : IPhoneDialer
    {
        public bool Dial(string i_Number)
        {
            return UIApplication.SharedApplication.OpenUrl(new NSUrl("tel:" + i_Number));
        }
    }
}
