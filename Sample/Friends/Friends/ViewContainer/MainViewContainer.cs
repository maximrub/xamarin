using Exrin.Abstraction;
using Friends.Locator;
using Friends.Stacks;

namespace Friends.ViewContainer
{
    public class MainViewContainer : Exrin.Framework.ViewContainer, ISingleContainer
    {

        public MainViewContainer(MainStack stack)
            : base(eContainer.Main.ToString(), stack.Proxy.NativeView)
        {
            Stack = stack;
        }

        public IStack Stack { get; set; }
    }
}