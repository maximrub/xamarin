using System.Threading.Tasks;
using Exrin.Abstraction;
using Exrin.Base;
using Friends.Domain.Phone.Interfaces;
using Friends.ModelState;
using Microsoft.Extensions.Logging;

namespace Friends.Model
{
    public class AboutModel : BaseModel, IAboutModel
    {
        private readonly IPhoneDialer r_PhoneDialer;
        private readonly ILogger<AboutModel> r_Logger;

        public AboutModel(IExrinContainer i_ExrinContainer, IPhoneDialer i_PhoneDialer, ILogger<AboutModel> i_Logger)
            : base(i_ExrinContainer, new AboutModelState())
        {
            r_PhoneDialer = i_PhoneDialer;
            r_Logger = i_Logger;
        }

        public IAboutModelState State
        {
            get
            {
                return ModelState as IAboutModelState;
            }
        }

        public async Task<bool> Call()
        {
            r_Logger.LogDebug("Calling {0}", State.SelectedPerson.FullName);
            return await Task.Factory.StartNew<bool>(() => r_PhoneDialer.Dial(State.SelectedPerson.Phone));
        }
    }
}