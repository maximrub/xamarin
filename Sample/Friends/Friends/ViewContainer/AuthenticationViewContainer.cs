using Exrin.Abstraction;
using Friends.Locator;
using Friends.Stacks;

namespace Friends.ViewContainer
{
    public class AuthenticationViewContainer : Exrin.Framework.ViewContainer, ISingleContainer
    {

        public AuthenticationViewContainer(AuthenticationStack i_Stack)
            : base(eContainer.Authentication.ToString(), i_Stack.Proxy.NativeView)
        {
            Stack = i_Stack;
        }

        public IStack Stack { get; set; }
    }
}