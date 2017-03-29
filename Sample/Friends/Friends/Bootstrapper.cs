using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Exrin.Abstraction;
using Exrin.IOC;
using Xamarin.Forms;

namespace Friends
{
    public class Bootstrapper : Exrin.Framework.Bootstrapper
    {
        private static Bootstrapper s_Instance = null;
        private readonly Func<object> r_RootGet;
        private readonly Action<object> r_RootSet;

        public Bootstrapper(IInjectionProxy i_Injection, Action<object> i_SetRoot, Func<object> i_GetRoot)
            : base(i_Injection, i_SetRoot, i_GetRoot)
        {
            r_RootGet = i_GetRoot;
            r_RootSet = i_SetRoot;
        }

        public static Bootstrapper Instance
        {
            get
            {
                if(s_Instance == null)
                {
                    s_Instance = new Bootstrapper(
                        new InjectionProxy(Exrin.IOC.LightInjectProvider.Instance.Container), 
                        (i_View) => Application.Current.MainPage = i_View as Page,
                        () => Application.Current.MainPage);
                }

                s_Instance.RegisterAssembly(AssemblyAction.Bootstrapper, new AssemblyRegister().GetType().GetTypeInfo().Assembly.GetName());

                return s_Instance;
            }
        }
    }
}
